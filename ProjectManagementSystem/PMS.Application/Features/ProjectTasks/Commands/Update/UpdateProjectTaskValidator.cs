using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Commands.Update;

public class UpdateProjectTaskValidator : AbstractValidator<UpdateProjectTaskCommand>
{
    public UpdateProjectTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Status).NotEmpty().IsInEnum();
    }
}
