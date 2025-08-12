using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.ProjectInvitations.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Application.Pipelines.Transaction;
using PMS.Core.Security.Entities;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.ProjectInvitations.Commands.Send;

public class SendProjectInvitationCommand : IRequest<SendProjectInvitationResponse>, ILoggableRequest, ITransactionalRequest, ISecuredRequest
{
    public string Email { get; set; }
    public Guid ProjectId { get; set; }
    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectOwner };

    public class SendProjectInvitationCommandHandler : IRequestHandler<SendProjectInvitationCommand, SendProjectInvitationResponse>
    {
        private readonly IProjectInvitationService _projectInvitationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectService _projectService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ProjectInvitationBusinessRules _businessRules;

        public SendProjectInvitationCommandHandler(IProjectInvitationService projectInvitationService, IMapper mapper, IProjectService projectService,IUserService userService, IHttpContextAccessor httpContextAccessor , IEmailService emailService ,ProjectInvitationBusinessRules businessRules)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectInvitationService = projectInvitationService;
            _userService  = userService;
            _projectService = projectService;
            _emailService = emailService;
            _mapper = mapper;
            _businessRules = businessRules;
        }

        public async Task<SendProjectInvitationResponse> Handle(SendProjectInvitationCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectService.GetAsync(x => x.Id == request.ProjectId, enableTracking: false, cancellationToken: cancellationToken);

            Guid ownerUserId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

            await _businessRules.UserShouldBeProjectOwnerWhenSendInvite(project.Id, ownerUserId);

            await _businessRules.ProjectShouldExist(project);

            User? user = await _userService.GetAsync(x => x.Email == request.Email, enableTracking: false, cancellationToken: cancellationToken);

            await _businessRules.UserShouldExist(user);
            await _businessRules.UserShouldntExistAlreadyInTheSameProject(user, project);

            ProjectInvitation invitation = _mapper.Map<ProjectInvitation>(request);
            invitation.UserId = user.Id;
            invitation.Id = Guid.NewGuid();
            invitation.ExpiredDate = DateTime.UtcNow.AddMinutes(30);

            ProjectInvitation result = await _projectInvitationService.AddAsync(invitation);

            SendProjectInvitationResponse response = _mapper.Map<SendProjectInvitationResponse>(result);

            await _emailService.SendProjectInvitationEmailAsync(user, invitation, project.ProjectName);

            return response;
        }
    }
}
