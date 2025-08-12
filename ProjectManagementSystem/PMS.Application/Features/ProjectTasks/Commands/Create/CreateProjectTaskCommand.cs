using AutoMapper;
using MediatR;
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

namespace PMS.Application.Features.ProjectTasks.Commands.Create;

public class CreateProjectTaskCommand : IRequest<CreateProjectTaskResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
    public ICollection<Guid> UserIds { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectOwner };

    public class CreateProjectTaskCommandHandler : IRequestHandler<CreateProjectTaskCommand, CreateProjectTaskResponse>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly IProjectTaskUserService _projectTaskUserService;
        private readonly ProjectTaskBusinessRules _projectTaskBusinessRules;
        private readonly IMapper _mapper;

        public CreateProjectTaskCommandHandler(IProjectTaskService projectTaskService, IMapper mapper, IProjectTaskUserService projectTaskUserService, ProjectTaskBusinessRules projectTaskBusinessRules)
        {
            _projectTaskService = projectTaskService;
            _mapper = mapper;
            _projectTaskUserService = projectTaskUserService;
            _projectTaskBusinessRules = projectTaskBusinessRules;
        }

        public async Task<CreateProjectTaskResponse> Handle(CreateProjectTaskCommand request, CancellationToken cancellationToken)
        {
            ProjectTask projectTask = _mapper.Map<ProjectTask>(request);
            projectTask.Status = ProjectTaskStatus.NotStarted;

            await _projectTaskBusinessRules.UserShouldBeProjectMember(request.ProjectId, request.UserIds);

            ProjectTask result = await _projectTaskService.AddAsync(projectTask);

            var projectTaskUsers = request.UserIds
                .Select(userId => new ProjectTaskUser
                    {
                        ProjectTaskId = result.Id,
                        UserId = userId,
                        ProjectId = result.ProjectId
                    })
                    .ToList();

            await _projectTaskUserService.AddRangeAsync(projectTaskUsers);

            CreateProjectTaskResponse response = _mapper.Map<CreateProjectTaskResponse>(result);

            return response;
        }
    }
}
