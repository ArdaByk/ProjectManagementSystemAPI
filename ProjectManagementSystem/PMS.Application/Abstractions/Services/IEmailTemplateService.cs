using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Abstractions.Services;

public interface IEmailTemplateService
{
    Task<string> GetProjectInvitationTemplateAsync(string projectName, string link);
    Task<string> GetEmailVerificationTemplateAsync(string link);
}
