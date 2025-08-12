using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsersQuery;
using PMS.Application.Features.Projects.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsers;

public class GetProjectWithProjectTasksAndUsersQuery : IRequest<GetProjectWithProjectTasksAndUsersResponse>, ISecuredRequest
{
    public Guid ProjectId { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember, OperationClaims.ProjectOwner };

    public class GetProjectWithProjectTasksAndUsersQueryHandler : IRequestHandler<GetProjectWithProjectTasksAndUsersQuery, GetProjectWithProjectTasksAndUsersResponse>
    {
        private readonly IProjectService _projectService;
        private readonly ProjectBusinessRules _projectBusinessRules;
        private readonly IMapper _mapper;

        public GetProjectWithProjectTasksAndUsersQueryHandler(IProjectService projectService, IMapper mapper, ProjectBusinessRules projectBusinessRules)
        {
            _projectService = projectService;
            _projectBusinessRules = projectBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetProjectWithProjectTasksAndUsersResponse> Handle(GetProjectWithProjectTasksAndUsersQuery request, CancellationToken cancellationToken)
        {
            Project? project = await _projectService.GetAsync(predicate: x => x.Id == request.ProjectId, include: x => x.Include(x => x.ProjectUsers).ThenInclude(x => x.User).Include(x => x.ProjectTasks).ThenInclude(x => x.ProjectTask).Include(x => x.ProjectTasks).ThenInclude(x => x.User), enableTracking: false, cancellationToken: cancellationToken);
        
            await _projectBusinessRules.ProjectShouldBeExistWhenSelected(project);

            GetProjectWithProjectTasksAndUsersResponse resposne = _mapper.Map<GetProjectWithProjectTasksAndUsersResponse>(project);

            return resposne;

        }
    }
}
