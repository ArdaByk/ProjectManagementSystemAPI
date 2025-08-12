using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Auth.Rules;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Hashing;
using PMS.Core.Security.JWT;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisterCommandResponse>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly ITokenHelper _tokenHelper;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailVerificationTokenService _emailVerificationTokenService;

        public RegisterCommandHandler(IUserService userService, IEmailService emailService, ITokenHelper tokenHelper, IEmailVerificationTokenService emailVerificationTokenService, AuthBusinessRules authBusinessRules)
        {
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _emailService = emailService;
            _tokenHelper = tokenHelper;
            _emailVerificationTokenService = emailVerificationTokenService;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserShouldntExistWhenRegister(request.Email);

            byte[] passwordHash;
            byte[] passwordSalt;

            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            User user = new()
                {
                    Email = request.Email,
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    EmailVerified = false
                };

            User result = await _userService.AddAsync(user);

            AccessToken createdAccessToken = _tokenHelper.CreateToken(user, new List<string> { OperationClaims.User });

            EmailVerificationToken emailVerificationToken = new EmailVerificationToken();
            emailVerificationToken.UserId = user.Id;
            emailVerificationToken.ExpireTime = DateTime.UtcNow.AddMinutes(10);

            EmailVerificationToken addEmailVerificationToken = await _emailVerificationTokenService.AddAsync(emailVerificationToken);

            await _emailService.SendEmailVerificationEmailAsync(user: result, emailVerificationToken);

            return new RegisterCommandResponse { AccessToken = createdAccessToken, Id = result.Id };
        }
    }
}
