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

namespace PMS.Application.Features.ProjectTasks.Queries.GetAProjectsTasksByUserId;

public class GetAProjectsTasksByUserIdQuery : IRequest<GetListResponse<GetAProjectsTasksByUserIdItemDto>>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public PageRequest PageRequest { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember, OperationClaims.ProjectOwner};

    public class GetListProjectTasksForAProjectByUserIdQueryHandler : IRequestHandler<GetAProjectsTasksByUserIdQuery, GetListResponse<GetAProjectsTasksByUserIdItemDto>>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly IMapper _mapper;

        public GetListProjectTasksForAProjectByUserIdQueryHandler(IProjectTaskService projectTaskService, IMapper mapper)
        {
            _projectTaskService = projectTaskService;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetAProjectsTasksByUserIdItemDto>> Handle(GetAProjectsTasksByUserIdQuery request, CancellationToken cancellationToken)
        {
            Paginate<ProjectTask> projectTasks = await _projectTaskService.GetListAsync(
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                predicate: x => x.Users.Any(x => x.User.Id == request.UserId) && x.ProjectId == request.ProjectId,
                include: x => x.Include(x => x.Project) ,
                enableTraking: false,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetAProjectsTasksByUserIdItemDto> response = _mapper.Map<GetListResponse<GetAProjectsTasksByUserIdItemDto>>(projectTasks);

            return response;
        }
    }
}
