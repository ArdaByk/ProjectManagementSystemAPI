using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Auth.Queries.VerifyEmail;

public class VerifyEmailQueryValidator : AbstractValidator<VerifyEmailQuery>
{
    public VerifyEmailQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
