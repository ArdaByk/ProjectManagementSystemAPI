using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Projects.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<GetProjectByIdResponse>, ISecuredRequest
{
    public Guid ProjectId { get; set; }
    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember, OperationClaims.ProjectOwner };

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
    {
        private readonly IProjectService _projectService;
        private readonly ProjectBusinessRules _projectBusinessRules;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IProjectService projectService, IMapper mapper, ProjectBusinessRules projectBusinessRules)
        {
            _projectService = projectService;
            _projectBusinessRules = projectBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            Project? project = await _projectService.GetAsync(x => x.Id == request.ProjectId, enableTracking: false, cancellationToken: cancellationToken);

            await _projectBusinessRules.ProjectShouldBeExistWhenSelected(project);

            GetProjectByIdResponse response = _mapper.Map<GetProjectByIdResponse>(project);

            return response;
        }
    }
}
