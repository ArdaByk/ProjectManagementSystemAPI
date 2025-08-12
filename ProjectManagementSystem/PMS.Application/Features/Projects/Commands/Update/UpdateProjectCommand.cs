using AutoMapper;
using MediatR;
using PMS.Application.Abstractions.Services;
using PMS.Application.Features.Projects.Rules;
using PMS.Core.Application.Pipelines.Authorization;
using PMS.Core.Application.Pipelines.Caching;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Commands.Update;

public class UpdateProjectCommand : IRequest<UpdateProjectResponse>, ILoggableRequest, ISecuredRequest
{
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public ProjectStatus ProjectStatus { get; set; }

    public Guid ProjectId { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectOwner };

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectResponse>
    {
        private readonly IProjectService _projectService;
        private readonly ProjectBusinessRules _projectBusinessRules;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IProjectService projectService, ProjectBusinessRules projectBusinessRules, IMapper mapper)
        {
            _projectService = projectService;
            _projectBusinessRules = projectBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdateProjectResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectService.GetAsync(x => x.Id == request.ProjectId, enableTracking: false, cancellationToken: cancellationToken);

            await _projectBusinessRules.ProjectShouldBeExistWhenSelected(project);
            
            project = _mapper.Map(request, project);
            
            Project result = await _projectService.UpdateAsync(project);

            UpdateProjectResponse response = _mapper.Map<UpdateProjectResponse>(result);

            return response;

        }
    }
}
