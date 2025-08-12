using PMS.Application.Abstractions.Services;
using PMS.Core.Security.Authorization;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Common.Authorization;

public class ProjectRoleProvider : IProjectRoleProvider
{
    private readonly IProjectUserService _projectUserService;

    public ProjectRoleProvider(IProjectUserService projectUserService)
    {
        _projectUserService = projectUserService;
    }

    public async Task<string>? GetUserOperationClaim(Guid? userId, Guid projectId)
    {
        ProjectUser? projectUser =  await _projectUserService.GetAsync(x => x.UserId == userId && x.ProjectId == projectId, enableTracking: false);

        return projectUser?.OperationClaim;
    }
}
