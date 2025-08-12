using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Auth.Rules;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Auth.Queries.VerifyEmail;

public class VerifyEmailQuery : IRequest<VerifyEmailResponse>, ILoggableRequest
{
    public Guid Id { get; set; }

    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailQuery, VerifyEmailResponse>
    {
        private readonly IEmailVerificationTokenService _emailVerificationTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public VerifyEmailCommandHandler(IEmailVerificationTokenService emailVerificationTokenService, IUserService userService, IHttpContextAccessor httpContextAccessor, IMapper mapper, AuthBusinessRules authBusinessRules)
        {
            _authBusinessRules = authBusinessRules;
            _emailVerificationTokenService = emailVerificationTokenService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public async Task<VerifyEmailResponse> Handle(VerifyEmailQuery request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

            EmailVerificationToken? token = await _emailVerificationTokenService.GetAsync(x => x.UserId == userId && x.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

            await _authBusinessRules.TokenShouldBeExist(token);

            await _authBusinessRules.ExpiredDateShouldNotBePast(token);

            await _authBusinessRules.MailShouldNotBeVerifiedWhenVerification(userId);

            User? user = await _userService.GetAsync(x => x.Id == userId, enableTracking: false, cancellationToken: cancellationToken);

            user.EmailVerified = true;

            await _userService.UpdateAsync(user);

            var result = await _emailVerificationTokenService.DeleteAsync(token, permanent: true);

            return new VerifyEmailResponse { Email = user.Email, UserId=userId };
        }
    }
}
