using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectTasks.Rules;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByUserId;

public class GetProjectTasksByUserIdQuery : IRequest<GetListResponse<GetProjectTasksByUserIdItemDto>>
{
    public Guid UserId { get; set; }
    public PageRequest PageRequest { get; set; }

    public class GetListByUserIdQueryHandler : IRequestHandler<GetProjectTasksByUserIdQuery, GetListResponse<GetProjectTasksByUserIdItemDto>>
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly ProjectTaskBusinessRules _projectTaskBusinessRules;
        private readonly IMapper _mapper;

        public GetListByUserIdQueryHandler(IProjectTaskService projectTaskService, IMapper mapper, ProjectTaskBusinessRules projectTaskBusinessRules)
        {
            _projectTaskService = projectTaskService;
            _projectTaskBusinessRules = projectTaskBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetProjectTasksByUserIdItemDto>> Handle(GetProjectTasksByUserIdQuery request, CancellationToken cancellationToken)
        {
            await _projectTaskBusinessRules.UserIdShouldBeTheSameWithCurrentUser(request.UserId);

            Paginate<ProjectTask> projectTasks = await _projectTaskService.GetListAsync(
                pageIndex: request.PageRequest.PageIndex,
                pageSize: request.PageRequest.PageSize,
                include: x => x.Include(x => x.Users).Include(x => x.Project),
                predicate: x => x.Users.Any(x => x.User.Id == request.UserId),
                enableTraking: false,
                cancellationToken: cancellationToken);

            GetListResponse<GetProjectTasksByUserIdItemDto> response = _mapper.Map<GetListResponse<GetProjectTasksByUserIdItemDto>>(projectTasks);
            return response;
        }
    }
}
