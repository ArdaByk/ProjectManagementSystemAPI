using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Commands.Delete;

public class DeleteProjectTaskValidator : AbstractValidator<DeleteProjectTaskCommand>
{
    public DeleteProjectTaskValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();

    }
}
