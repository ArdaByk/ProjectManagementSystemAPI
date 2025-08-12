using Application.Features.Auth.Constants;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectInvitations.Constants;
using PMS.Core.Application.Rules;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using PMS.Core.Security.Entities;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Rules;

public class ProjectInvitationBusinessRules : BaseBusinessRule
{
    private readonly IProjectUserService _projectUserService;

    public ProjectInvitationBusinessRules(IProjectUserService projectUserService)
    {
        _projectUserService = projectUserService;
    }
    public async Task ProjectInvitationShouldExist(ProjectInvitation? projectInvitation)
    {
        if (projectInvitation == null)
            throw new BusinessException(ProjectInvitationMessages.InvitationDoesntExist); 
    }
    public async Task UserShouldExist(User? user)
    {
        if (user == null)
            throw new BusinessException(ProjectInvitationMessages.UserDoesntExist);
    }
    public async Task ProjectShouldExist(Project? project)
    {
        if (project == null)
            throw new BusinessException(ProjectInvitationMessages.ProjectDoesntExist);
    }
    public async Task UserShouldntExistAlreadyInTheSameProject(User user, Project project)
    {
        ProjectUser? projectUser = await _projectUserService.GetAsync(x => x.UserId == user.Id && x.ProjectId == project.Id, enableTracking: false);

        if(projectUser != null)
            throw new BusinessException(ProjectInvitationMessages.UserAlreadyExists);
    }
    public async Task ExpiredDateShouldNotBePast(ProjectInvitation invitation)
    {
        if(DateTime.UtcNow > invitation.ExpiredDate)
            throw new BusinessException(ProjectInvitationMessages.InvitationExpired);
    }
    public async Task UserShouldBeProjectOwnerWhenSendInvite(Guid projectId, Guid userId)
    {
        bool projectUser = await _projectUserService.AnyAsync(x => x.UserId == userId && x.ProjectId == projectId && x.OperationClaim == OperationClaims.ProjectOwner, enableTraking: true);

        if(!projectUser)
            throw new BusinessException(AuthMessages.AccessDenied);
    }
}
