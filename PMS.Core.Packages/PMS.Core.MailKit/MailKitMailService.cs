
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PMS.Core.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.MailKit;

public class MailKitMailService : IMailService
{
    private readonly MailSettings _settings;

    public MailKitMailService(IConfiguration configuration)
    {
        _settings = configuration.GetSection("MailSettings").Get<MailSettings>() ?? throw new Exception("Mail Configurations cannot be empty.");
    }

    public void SendMail(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;
        emailPrepare(mail, email: out MimeMessage email, smtp: out SmtpClient smtp);
        smtp.Send(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    public async Task SendMailAsync(Mail mail)
    {
        if (mail.ToList == null || mail.ToList.Count < 1)
            return;
        emailPrepare(mail, email: out MimeMessage email, smtp: out SmtpClient smtp);
        await smtp.SendAsync(email);
        smtp.Disconnect(true);
        email.Dispose();
        smtp.Dispose();
    }

    private void emailPrepare(Mail mail, out MimeMessage email, out SmtpClient smtp)
    {
        email = new MimeMessage();
        email.From.Add(new MailboxAddress(_settings.SenderFullName, _settings.SenderEmail));
        email.To.AddRange(mail.ToList);

        email.Subject = mail.Subject;

        var builder = new BodyBuilder();

        builder.TextBody = mail.TextBody;
        builder.HtmlBody = mail.HtmlBody;

        if (mail.Attachments != null)
            foreach (MimeEntity? attachment in mail.Attachments)
                if (attachment != null)
                    builder.Attachments.Add(attachment);

        email.Body = builder.ToMessageBody();

        smtp = new SmtpClient();
        smtp.Connect(_settings.Server, _settings.Port);

        if (_settings.AuthenticationRequired)
            smtp.Authenticate(_settings.UserName, _settings.Password);
    }
}
