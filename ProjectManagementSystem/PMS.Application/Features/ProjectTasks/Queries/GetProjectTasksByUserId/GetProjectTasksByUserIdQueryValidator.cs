using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByUserId;

public class GetProjectTasksByUserIdQueryValidator : AbstractValidator<GetProjectTasksByUserIdQuery>
{
    public GetProjectTasksByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.PageRequest).NotEmpty();
    }
}
