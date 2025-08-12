using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
{
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            HashingHelper.CreatePasswordHash(request.Password, passwordHash: out byte[] passwordHash, passwordSalt: out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            User createdUser = await _userService.UpdateAsync(user);

            UpdateUserCommandResponse response = _mapper.Map<UpdateUserCommandResponse>(createdUser);
            return response;
        }
    }
}