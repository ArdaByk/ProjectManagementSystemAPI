using Application.Features.Auth.Constants;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectTasks.Constants;
using PMS.Core.Application.Rules;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectTasks.Rules;

public class ProjectTaskBusinessRules : BaseBusinessRule
{
    private readonly IProjectTaskService _projectTaskService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProjectUserService _projectUserService;

    public ProjectTaskBusinessRules(IProjectTaskService projectTaskService, IHttpContextAccessor httpContextAccessor, IProjectUserService projectUserService)
    {
        _projectTaskService = projectTaskService;
        _httpContextAccessor = httpContextAccessor;
        _projectUserService = projectUserService;
    }
    public async Task UserIdShouldBeTheSameWithCurrentUser(Guid userId)
    {
        Guid currentUserId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

        if (!currentUserId.Equals(userId))
            throw new BusinessException(AuthMessages.AccessDenied);
    }
    public async Task ProjectTaskShouldBeExistWhenDeleted(ProjectTask? projectTask)
    {
        if (projectTask == null)
            throw new BusinessException(ProjectTaskMessages.ProjectTaskDoesNotExist);
    }
    public async Task ProjectTaskShouldBeExistWhenUpdated(ProjectTask? projectTask)
    {
        if (projectTask == null)
            throw new BusinessException(ProjectTaskMessages.ProjectTaskDoesNotExist);
    }
    public async Task ProjectTaskShouldBeExistWhenSelected(ProjectTask? projectTask)
    {
        if (projectTask == null)
            throw new BusinessException(ProjectTaskMessages.ProjectTaskDoesNotExist);
    }

    public async Task UserShouldBeProjectMember(Guid projectId, ICollection<Guid> userIds)
    {
        foreach (var userId in userIds)
        {
            bool projectUser = await _projectUserService.AnyAsync(x => x.UserId == userId && x.ProjectId == projectId, enableTraking: false);

            if (!projectUser)
                throw new BusinessException(ProjectTaskMessages.YouCanJustAddProjectMembers);
        }
    }
}
