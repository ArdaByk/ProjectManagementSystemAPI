using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Auth.Rules;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Security.Entities;
using PMS.Core.Security.JWT;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginCommandResponse>, ILoggableRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserService userService, ITokenHelper tokenHelper, AuthBusinessRules authBusinessRules)
        {
            _userService = userService;
            _authBusinessRules = authBusinessRules;
            _tokenHelper = tokenHelper;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(predicate: x => x.Email == request.Email, enableTracking: false, cancellationToken: cancellationToken);

            await _authBusinessRules.UserShouldExistWhenLogin(user);

            await _authBusinessRules.PasswordShouldBeCorrect(user, request.Password);

            await _authBusinessRules.EmailShouldBeVerifiedWhenLogin(user);

            AccessToken createdAccessToken = _tokenHelper.CreateToken(user, new List<string> { OperationClaims.User });

            return new LoginCommandResponse { AccessToken = createdAccessToken, UserId = user.Id };
        }
    }
}
