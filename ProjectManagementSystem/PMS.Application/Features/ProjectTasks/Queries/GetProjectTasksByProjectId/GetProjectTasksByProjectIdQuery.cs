using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectTasks.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByProjectId;

public class GetProjectTasksByProjectIdQuery : IRequest<GetListResponse<GetProjectTasksByProjectIdResponse>>, ISecuredRequest
{
    public Guid ProjectId { get; set; }
    public PageRequest PageRequest { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember, OperationClaims.ProjectOwner };

    public class GetProjectTasksByProjectIdQueryHandler : IRequestHandler<GetProjectTasksByProjectIdQuery, GetListResponse<GetProjectTasksByProjectIdResponse>>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly ProjectTaskBusinessRules _projectTaskBusinessRules;
        private readonly IMapper _mapper;

        public GetProjectTasksByProjectIdQueryHandler(IProjectTaskService projectTaskService, IMapper mapper, ProjectTaskBusinessRules projectTaskBusinessRules)
        {
            _projectTaskService = projectTaskService;
            _projectTaskBusinessRules = projectTaskBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetProjectTasksByProjectIdResponse>> Handle(GetProjectTasksByProjectIdQuery request, CancellationToken cancellationToken)
        {
            Paginate<ProjectTask>? projectTasks = await _projectTaskService.GetListAsync(pageSize:request.PageRequest.PageSize, pageIndex: request.PageRequest.PageIndex, predicate: x => x.ProjectId == request.ProjectId, include: x => x.Include(x => x.Users).ThenInclude(x => x.User) ,enableTraking: false, cancellationToken: cancellationToken);

            GetListResponse<GetProjectTasksByProjectIdResponse> response = _mapper.Map<GetListResponse<GetProjectTasksByProjectIdResponse>>(projectTasks);

            return response;
        }
    }
}
