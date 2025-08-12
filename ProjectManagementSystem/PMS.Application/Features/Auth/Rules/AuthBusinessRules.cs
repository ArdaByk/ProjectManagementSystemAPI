using Application.Features.Auth.Constants;
using MimeKit;
using PMS.Application.Abstractions.Services;
using PMS.Core.Application.Rules;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using PMS.Core.Mailing;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Auth.Rules;

public class AuthBusinessRules : BaseBusinessRule
{
    private readonly IUserService _userService;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IMailService _mailService;
    private readonly IEmailVerificationTokenService _emailVerificationTokenService;

    public AuthBusinessRules(IUserService userService, IEmailTemplateService emailTemplateService, IMailService mailService, IEmailVerificationTokenService emailVerificationTokenService)
    {
        _userService = userService;
        _emailTemplateService = emailTemplateService;
        _mailService = mailService;
        _emailVerificationTokenService = emailVerificationTokenService;
    }
    public async Task UserShouldExistWhenLogin(User? user)
    {
        if (user == null)
            throw new BusinessException(AuthMessages.UserDontExists);
    }
    
    public async Task PasswordShouldBeCorrect(User user, string password)
    {
       bool isPasswordCorrect = HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

        if (!isPasswordCorrect)
            throw new BusinessException(AuthMessages.PasswordDontMatch);
    }
    public async Task EmailShouldBeVerifiedWhenLogin(User user)
    {
        if (!user.EmailVerified)
        {
            EmailVerificationToken? oldToken = await _emailVerificationTokenService.GetAsync(x => x.UserId ==  user.Id, enableTracking: false);
            await _emailVerificationTokenService.DeleteAsync(oldToken, permanent: true);

            EmailVerificationToken emailVerificationToken = new EmailVerificationToken();
            emailVerificationToken.UserId = user.Id;
            emailVerificationToken.ExpireTime = DateTime.UtcNow.AddMinutes(10);

            EmailVerificationToken addEmailVerificationToken = await _emailVerificationTokenService.AddAsync(emailVerificationToken);

            Mail mail = new Mail
            {
                Subject = $"Email Verification for Registration",
                HtmlBody = await _emailTemplateService.GetEmailVerificationTemplateAsync(addEmailVerificationToken.Id.ToString()),
                ToList = new List<MailboxAddress>
                {
                    new MailboxAddress(user.Username, user.Email)
                }
            };

            await _mailService.SendMailAsync(mail);
            throw new BusinessException(AuthMessages.EmailIsNotVerified);
        }
            
    }

    public async Task UserShouldntExistWhenRegister(string email)
    {
        User? user = await _userService.GetAsync(x => x.Email == email, enableTracking: false);

        if (user != null)
            throw new BusinessException(AuthMessages.UserAlreadyExists);
    }
    public async Task MailShouldNotBeVerifiedWhenVerification(Guid id)
    {
        User user = await _userService.GetAsync(x => x.Id == id, enableTracking: false);

        if (user.EmailVerified)
            throw new BusinessException(AuthMessages.MailAlreadyVerified);
    }
    public async Task ExpiredDateShouldNotBePast(EmailVerificationToken token)
    {
        if (DateTime.UtcNow > token.ExpireTime)
            throw new BusinessException(AuthMessages.TokenExpired);
    }

    public async Task TokenShouldBeExist(EmailVerificationToken? token)
    {
        if (token == null)
            throw new BusinessException(AuthMessages.TokenDoesntExist);
    }
}
