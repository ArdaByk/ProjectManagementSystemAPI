using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetAProjectsTasksByUserId;

internal class GetAProjectsTasksByUserIdQueryValidator : AbstractValidator<GetAProjectsTasksByUserIdQuery>
{
    public GetAProjectsTasksByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.PageRequest).NotEmpty();
    }
}
