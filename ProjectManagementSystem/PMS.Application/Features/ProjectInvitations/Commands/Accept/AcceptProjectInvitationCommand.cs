using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectInvitations.Rules;
using PMS.Core.Application.Pipelines.Caching;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Application.Pipelines.Transaction;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Commands.Accept;

public class AcceptProjectInvitationCommand : IRequest<AcceptProjectInvitationResponse>, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class AcceptProjectInvitationCommandHandler : IRequestHandler<AcceptProjectInvitationCommand, AcceptProjectInvitationResponse>
    {
        private readonly IProjectInvitationService _projectInvitationService;
        private readonly IProjectService _projectService;
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ProjectInvitationBusinessRules _projectInvitationBusinessRules;

        public AcceptProjectInvitationCommandHandler(IProjectInvitationService projectInvitationService, IProjectUserService projectUserService ,IProjectService projectService ,IHttpContextAccessor httpContextAccessor, ProjectInvitationBusinessRules projectInvitationBusinessRules)
        {
            _projectUserService = projectUserService;
            _projectService = projectService;
            _httpContextAccessor = httpContextAccessor;
            _projectInvitationService = projectInvitationService;
            _projectInvitationBusinessRules = projectInvitationBusinessRules;
        }

        public async Task<AcceptProjectInvitationResponse> Handle(AcceptProjectInvitationCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.GetIdClaim();

            ProjectInvitation? invitation = await _projectInvitationService.GetAsync(x => x.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

            await _projectInvitationBusinessRules.ProjectInvitationShouldExist(invitation);

            ProjectUser projectUser = new ProjectUser { ProjectId = invitation.ProjectId, UserId = Guid.Parse(userId), OperationClaim = OperationClaims.ProjectMember };

            await _projectInvitationBusinessRules.ExpiredDateShouldNotBePast(invitation);

            await _projectUserService.AddAsync(projectUser);

            Project currentProject = await _projectService.GetAsync(x => x.Id == projectUser.ProjectId, enableTracking: false, cancellationToken: cancellationToken);

            await _projectService.UpdateAsync(currentProject);

            await _projectInvitationService.DeleteAsync(invitation);

            return new AcceptProjectInvitationResponse();

        }
    }
}
