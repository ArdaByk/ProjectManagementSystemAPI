using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Core.Application.Pipelines.Caching;
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

namespace PMS.Application.Features.Projects.Queries.GetProjectsByUserId;

public class GetProjectsByUserIdQuery : IRequest<GetListResponse<GetProjectsByUserIdItemDto>>
{
    public Guid UserId { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetProjectsByUserIdQueryHandler : IRequestHandler<GetProjectsByUserIdQuery, GetListResponse<GetProjectsByUserIdItemDto>>
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public GetProjectsByUserIdQueryHandler(IProjectUserService projectUserService, IProjectService projectService, IMapper mapper)
        {
            _projectUserService = projectUserService;
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetProjectsByUserIdItemDto>> Handle(GetProjectsByUserIdQuery request, CancellationToken cancellationToken)
        {
            ICollection<ProjectUser> projectUsers = await _projectUserService.GetAllAsync(predicate: x => x.UserId == request.UserId && x.OperationClaim == OperationClaims.ProjectMember,
               enableTraking: false,
               cancellationToken: cancellationToken);

            var projectIds = projectUsers.Select(x => x.ProjectId).ToList();

            Paginate<Project> projects = await _projectService.GetListAsync(
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                predicate: x => projectIds.Contains(x.Id),
                enableTraking: false,
                cancellationToken: cancellationToken
                );

            GetListResponse<GetProjectsByUserIdItemDto> response = _mapper.Map<GetListResponse<GetProjectsByUserIdItemDto>>(projects);

            return response;
        }
    }
}
