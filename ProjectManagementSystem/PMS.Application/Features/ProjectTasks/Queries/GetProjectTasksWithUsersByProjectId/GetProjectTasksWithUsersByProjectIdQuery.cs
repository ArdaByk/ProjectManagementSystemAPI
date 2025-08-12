using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Abstractions.Services;
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

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksWithUsersByProjectId;

public class GetProjectTasksWithUsersByProjectIdQuery : IRequest<GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto>>, ISecuredRequest
{
    public Guid ProjectId { get; set; }
    public PageRequest PageRequest { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember, OperationClaims.ProjectOwner};

    public class GetListTasksWithUsersByProjectIdQueryHandler : IRequestHandler<GetProjectTasksWithUsersByProjectIdQuery, GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto>>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly IMapper _mapper;

        public GetListTasksWithUsersByProjectIdQueryHandler(IProjectTaskService projectTaskService, IMapper mapper)
        {
            _projectTaskService = projectTaskService;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto>> Handle(GetProjectTasksWithUsersByProjectIdQuery request, CancellationToken cancellationToken)
        {
            Paginate<ProjectTask> projectTasks = await _projectTaskService.GetListAsync(
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                predicate: x => x.ProjectId == request.ProjectId,
                include: x => x.Include(x => x.Users).ThenInclude(x => x.User),
                enableTraking: false,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto> response = _mapper.Map<GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto>>(projectTasks);

            return response;
        }
    }
}
