using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Commands.Accept;

public class AcceptProjectInvitationCommandValidator : AbstractValidator<AcceptProjectInvitationCommand>
{
    public AcceptProjectInvitationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
