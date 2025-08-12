using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectTasks.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Core.Application.Pipelines.Caching;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Application.Pipelines.Transaction;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Commands.Update;

public class UpdateProjectTaskCommand : IRequest<UpdateProjectTaskResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
    public ICollection<Guid> UserIds { get; set; }
    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectOwner};

    public class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand, UpdateProjectTaskResponse>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly ProjectTaskBusinessRules _projectTaskBusinessRules;
        private readonly IMapper _mapper;

        public UpdateProjectTaskCommandHandler(IProjectTaskService projectTaskService, IMapper mapper, ProjectTaskBusinessRules projectTaskBusinessRules)
        {
            _projectTaskService = projectTaskService;
            _projectTaskBusinessRules = projectTaskBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdateProjectTaskResponse> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            ProjectTask? projectTask = await _projectTaskService.GetAsync(x => x.Id == request.Id, include: x => x.Include(x => x.Users), cancellationToken: cancellationToken);

            await _projectTaskBusinessRules.ProjectTaskShouldBeExistWhenUpdated(projectTask);

            var existingUserIds = projectTask.Users.Select(ptu => ptu.UserId).ToList();
            var userIdsToRemove = existingUserIds.Except(request.UserIds).ToList();
            var userIdsToAdd = request.UserIds.Except(existingUserIds).ToList();

            foreach (var userId in userIdsToRemove)
            {
                var userToRemove = projectTask.Users.FirstOrDefault(ptu => ptu.UserId == userId);
                if (userToRemove != null)
                {
                    projectTask.Users.Remove(userToRemove);
                }
            }

            foreach (var userId in userIdsToAdd)
            {
                projectTask.Users.Add(new ProjectTaskUser
                {
                    ProjectTaskId = projectTask.Id,
                    UserId = userId,
                    ProjectId = projectTask.ProjectId
                });
            }

            _mapper.Map(request, projectTask);

            ProjectTask result = await _projectTaskService.UpdateAsync(projectTask);

            UpdateProjectTaskResponse response = _mapper.Map<UpdateProjectTaskResponse>(result);

            return response;
        }
    }
}
