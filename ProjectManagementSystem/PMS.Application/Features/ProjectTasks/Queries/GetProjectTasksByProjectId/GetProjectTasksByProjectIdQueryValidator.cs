using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByProjectId;

public class GetProjectTasksByProjectIdQueryValidator : AbstractValidator<GetProjectTasksByProjectIdQuery>
{
    public GetProjectTasksByProjectIdQueryValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
    }
}
