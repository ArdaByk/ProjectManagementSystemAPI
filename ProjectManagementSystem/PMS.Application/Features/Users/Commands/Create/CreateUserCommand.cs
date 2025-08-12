using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Users.Rules;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreateUserResponse>
{
    public string Username { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userService = userService;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Mail);

            User user = _mapper.Map<User>(request);
            user.EmailVerified = false;

            HashingHelper.CreatePasswordHash(request.Password, passwordHash: out byte[] passwordHash, passwordSalt: out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            User createdUser = await _userService.AddAsync(user);

            CreateUserResponse response = _mapper.Map<CreateUserResponse>(createdUser);
            return response;
        }
    }
}