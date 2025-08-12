using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTaskById;

public class GetProjectTaskByIdQueryValidator : AbstractValidator<GetProjectTaskByIdQuery>
{
    public GetProjectTaskByIdQueryValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
    }
}
