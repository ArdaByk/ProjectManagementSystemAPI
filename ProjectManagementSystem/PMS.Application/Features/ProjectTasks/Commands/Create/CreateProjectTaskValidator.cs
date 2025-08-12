using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Commands.Create;

public class CreateProjectTaskValidator : AbstractValidator<CreateProjectTaskCommand>
{
    public CreateProjectTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.UserIds).NotEmpty();
        RuleFor(x => x.ProjectId).NotEmpty();
    }
}
