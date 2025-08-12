using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksWithUsersByProjectId;

public class GetProjectTasksWithUsersByProjectIdQueryValidator : AbstractValidator<GetProjectTasksWithUsersByProjectIdQuery>
{
    public GetProjectTasksWithUsersByProjectIdQueryValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.PageRequest).NotEmpty();
    }
}
