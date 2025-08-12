using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;
using PMS.Core.Persistence.Paging;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Queries.GetList;

public class GetListUserQuery : IRequest<GetListResponse<GetListUserQueryItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListResponse<GetListUserQueryItemDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetListUserQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserQueryItemDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
        {
            Paginate<User> users = await _userService.GetListAsync(pageIndex: request.PageRequest.PageIndex, pageSize: request.PageRequest.PageSize,
                 enableTraking: false, cancellationToken: cancellationToken);

            GetListResponse<GetListUserQueryItemDto> response = _mapper.Map<GetListResponse<GetListUserQueryItemDto>>(users);

            return response;
        }
    }
}
