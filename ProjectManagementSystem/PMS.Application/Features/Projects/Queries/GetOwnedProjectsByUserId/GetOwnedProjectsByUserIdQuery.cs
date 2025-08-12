using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Core.Application.Pipelines.Caching;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Queries.GetOwnedProjectsByUserId;

public class GetOwnedProjectsByUserIdQuery : IRequest<GetListResponse<GetOwnedProjectsByUserIdResponse>>
{
    public PageRequest PageRequest { get; set; }
    public class GetOwnedProjectsByUserIdQueryHandler : IRequestHandler<GetOwnedProjectsByUserIdQuery, GetListResponse<GetOwnedProjectsByUserIdResponse>>
    {
        private readonly IProjectService _projectService;
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetOwnedProjectsByUserIdQueryHandler(IProjectService projectService, IMapper mapper, IProjectUserService projectUserService, IHttpContextAccessor httpContextAccessor)
        {
            _projectService = projectService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _projectUserService = projectUserService;
        }

        public async Task<GetListResponse<GetOwnedProjectsByUserIdResponse>> Handle(GetOwnedProjectsByUserIdQuery request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

            Paginate<ProjectUser> projectUsers = await _projectUserService.GetListAsync(
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                predicate: x => x.UserId == userId && x.OperationClaim == OperationClaims.ProjectOwner,
                enableTraking: false,
                cancellationToken: cancellationToken
                );

            var projectIds = projectUsers.Items.Select(x => x.ProjectId).ToList();

            Paginate<Project> projects = await _projectService.GetListAsync(
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                predicate: x => projectIds.Contains(x.Id)
                );

            GetListResponse<GetOwnedProjectsByUserIdResponse> response = _mapper.Map<GetListResponse<GetOwnedProjectsByUserIdResponse>>(projects);

            return response;
        }
    }
}