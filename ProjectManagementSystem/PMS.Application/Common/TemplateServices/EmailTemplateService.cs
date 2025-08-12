using Microsoft.Extensions.Configuration;
using PMS.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Common.TemplateServices;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly string _templatePath;
    private readonly EmailTemplateConfiguration _configuration;

    public EmailTemplateService(IConfiguration configuration)
    {
        _templatePath = Path.Combine(AppContext.BaseDirectory, "Common", "Templates");
        _configuration = configuration.GetSection("EmailTemplateConfigurations").Get<EmailTemplateConfiguration>();
    }

    public async Task<string> GetProjectInvitationTemplateAsync(string projectName, string invitationId)
    {
        var html = await File.ReadAllTextAsync(_templatePath+"/TeamInvitation.html");
        html = html.Replace("{{ProjectName}}", projectName);
        html = html.Replace("{{invitationId}}", invitationId);
        html = html.Replace("{{url}}", _configuration.AcceptInvitationFrontendUrl);

        return html;
    }
    public async Task<string> GetEmailVerificationTemplateAsync(string verificationId)
    {
        var html = await File.ReadAllTextAsync(_templatePath+"/EmailVerification.html");
        html = html.Replace("{{url}}", _configuration.EmailVerificationFrontendUrl);
        html = html.Replace("{{verificationId}}", verificationId);

        return html;
    }
}
