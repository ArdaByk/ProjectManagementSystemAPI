using PMS.Core.Security.Entities;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendEmailVerificationEmailAsync(User user, EmailVerificationToken emailVerificationToken);
    Task SendProjectInvitationEmailAsync(User user, ProjectInvitation projectInvitation, string projectName);
}
