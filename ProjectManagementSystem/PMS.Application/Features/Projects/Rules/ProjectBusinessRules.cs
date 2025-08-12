using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Projects.Constants;
using PMS.Core.Application.Rules;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Rules;

public class ProjectBusinessRules : BaseBusinessRule
{
    private readonly IProjectService _projectService;

    public ProjectBusinessRules(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task ProjectShouldBeExistWhenDeleted(Project? project)
    {
        if (project == null)
            throw new BusinessException(ProjectMessages.ProjectDoesNotExist);
    }
    public async Task ProjectShouldBeExistWhenUpdated(Guid id)
    {
        var project = await _projectService.AnyAsync(x => x.Id == id);

        if (!project)
            throw new BusinessException(ProjectMessages.ProjectDoesNotExist);
    }
    public async Task ProjectShouldBeExistWhenSelected(Project? project)
    {
        if (project == null)
            throw new BusinessException(ProjectMessages.ProjectDoesNotExist);
    }
}