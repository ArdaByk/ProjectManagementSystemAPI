using MimeKit;
using PMS.Application.Abstractions.Services;
using PMS.Core.Mailing;
using PMS.Core.Security.Entities;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infrastructure;

public class EmailService : IEmailService
{
    private readonly Core.Mailing.IMailService _mailService;
    private readonly IEmailTemplateService _emailTemplateService;

    public EmailService(Core.Mailing.IMailService mailService, IEmailTemplateService emailTemplateService)
    {
        _mailService = mailService;
        _emailTemplateService = emailTemplateService;
    }

    public async Task SendEmailVerificationEmailAsync(User user, EmailVerificationToken emailVerificationToken)
    {

        Mail mail = new Mail
        {
            Subject = $"Email Verification for Registration",
            HtmlBody = await _emailTemplateService.GetEmailVerificationTemplateAsync(emailVerificationToken.Id.ToString()),
            ToList = new List<MailboxAddress>
                {
                    new MailboxAddress(user.Username, user.Email)
                }
        };

        await _mailService.SendMailAsync(mail);
    }

    public async Task SendProjectInvitationEmailAsync(User user, ProjectInvitation projectInvitation ,string projectName)
    {
        Mail mail = new Mail
        {
            Subject = $"Invitation to participate in the {projectName} project",
            HtmlBody = await _emailTemplateService.GetProjectInvitationTemplateAsync(projectName, projectInvitation.Id.ToString()),
            ToList = new List<MailboxAddress>
                {
                    new MailboxAddress(user.Username, user.Email)
                }
        };

        await _mailService.SendMailAsync(mail);
    }
}
