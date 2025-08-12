using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectUsers.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectUsers.Commands.LeaveProject;

public class LeaveProjectCommand : IRequest<LeaveProjectCommandResponse>, ISecuredRequest
{
    public Guid ProjectId { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectMember };

    public class LeaveProjectCommandHandler : IRequestHandler<LeaveProjectCommand, LeaveProjectCommandResponse>
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IProjectTaskUserService _projectTaskUserService;
        private readonly ProjectUserBusinessRules _projectUserBusinessRules;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectService _projectService;

        public LeaveProjectCommandHandler(IProjectUserService projectUserService, ProjectUserBusinessRules projectUserBusinessRules, IHttpContextAccessor httpContextAccessor, IProjectService projectService, IProjectTaskUserService projectTaskUserService)
        {
            _projectUserService = projectUserService;
            _projectUserBusinessRules = projectUserBusinessRules;
            _httpContextAccessor = httpContextAccessor;
            _projectService = projectService;
            _projectTaskUserService = projectTaskUserService;
        }

        public async Task<LeaveProjectCommandResponse> Handle(LeaveProjectCommand request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

            ProjectUser? projectUser = await _projectUserService.GetAsync(x => x.UserId == userId && x.ProjectId == request.ProjectId, enableTracking: false, cancellationToken:cancellationToken);

            await _projectUserBusinessRules.ProjectUserShouldExist(projectUser);

            ProjectUser result = await _projectUserService.DeleteAsync(projectUser, permanent: true);

            Project project = await _projectService.GetAsync(x => x.Id == projectUser.ProjectId, enableTracking: false, cancellationToken: cancellationToken);

            await _projectService.UpdateAsync(project);

            return new LeaveProjectCommandResponse { Id = result.Id};
        }
    }
}
