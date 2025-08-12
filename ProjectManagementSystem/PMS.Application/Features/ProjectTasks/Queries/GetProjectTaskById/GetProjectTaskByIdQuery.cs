using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectTasks.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTaskById;

public class GetProjectTaskByIdQuery : IRequest<GetProjectTaskByIdResponse>, ISecuredRequest
{
    public Guid ProjectTaskId { get; set; }

    public Guid ProjectId { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember, OperationClaims.ProjectOwner };

    public class GetProjectTaskByIdQueryHandler : IRequestHandler<GetProjectTaskByIdQuery, GetProjectTaskByIdResponse>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly ProjectTaskBusinessRules _projectTaskBusinessRules;
        private readonly IMapper _mapper;

        public GetProjectTaskByIdQueryHandler(IProjectTaskService projectTaskService, IMapper mapper, ProjectTaskBusinessRules projectTaskBusinessRules)
        {
            _projectTaskService = projectTaskService;
            _projectTaskBusinessRules = projectTaskBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetProjectTaskByIdResponse> Handle(GetProjectTaskByIdQuery request, CancellationToken cancellationToken)
        {
            ProjectTask? projectTask = await _projectTaskService.GetAsync(x => x.Id == request.ProjectTaskId && x.ProjectId == request.ProjectId, include: x => x.Include(x => x.Users) ,enableTracking: false, cancellationToken: cancellationToken);

            await _projectTaskBusinessRules.ProjectTaskShouldBeExistWhenSelected(projectTask);

            GetProjectTaskByIdResponse response = _mapper.Map<GetProjectTaskByIdResponse>(projectTask);

            return response;
        }
    }
}
