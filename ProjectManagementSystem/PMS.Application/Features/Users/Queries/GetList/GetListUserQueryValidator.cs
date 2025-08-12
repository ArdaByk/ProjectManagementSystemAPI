using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Users.Queries.GetList;

public class GetListUserQueryValidator : AbstractValidator<GetListUserQuery>
{
    public GetListUserQueryValidator()
    {
        RuleFor(x => x.PageRequest).NotEmpty();
    }
}
