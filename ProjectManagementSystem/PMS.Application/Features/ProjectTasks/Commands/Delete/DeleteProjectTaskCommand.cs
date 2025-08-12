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

namespace PMS.Application.Features.ProjectTasks.Commands.Delete;

public class DeleteProjectTaskCommand : IRequest<DeleteProjectTaskResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public Guid ProjectTaskId { get; set; }
    public Guid ProjectId { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectOwner };

    public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand, DeleteProjectTaskResponse>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly ProjectTaskBusinessRules _projectTaskBusinessRules;
        private readonly IMapper _mapper;

        public DeleteProjectTaskCommandHandler(IProjectTaskService projectTaskService, IMapper mapper, ProjectTaskBusinessRules projectTaskBusinessRules)
        {
            _projectTaskService = projectTaskService;
            _projectTaskBusinessRules = projectTaskBusinessRules;
            _mapper = mapper;
        }

        public async Task<DeleteProjectTaskResponse> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken)
        {
            ProjectTask? projectTask = await _projectTaskService.GetAsync(predicate: x => x.Id == request.ProjectTaskId, enableTracking: false, cancellationToken: cancellationToken);

            await _projectTaskBusinessRules.ProjectTaskShouldBeExistWhenDeleted(projectTask);

            ProjectTask result = await _projectTaskService.DeleteAsync(projectTask);

            DeleteProjectTaskResponse response = _mapper.Map<DeleteProjectTaskResponse>(result);

            return response;
        }
    }
}
