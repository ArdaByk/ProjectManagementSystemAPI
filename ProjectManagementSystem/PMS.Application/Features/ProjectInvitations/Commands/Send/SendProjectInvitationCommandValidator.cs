using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Commands.Send;

public class AcceptProjectInvitationCommandValidator : AbstractValidator<SendProjectInvitationCommand>
{
    public AcceptProjectInvitationCommandValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
    }
}
