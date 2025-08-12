using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery:IRequest<GetByIdUserResponse>
{
    public Guid Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetByIdUserQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(x => x.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

            GetByIdUserResponse result = _mapper.Map<GetByIdUserResponse>(user);

            return result;
        }
    }
}
