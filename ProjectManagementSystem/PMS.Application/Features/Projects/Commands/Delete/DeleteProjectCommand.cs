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

namespace PMS.Application.Features.Projects.Commands.Delete;

public class DeleteProjectCommand : IRequest<DeleteProjectResponse>, ILoggableRequest, ISecuredRequest
{
    public Guid ProjectId { get; set; }

    public ICollection<string> RequiredProjectRoles => new List<string> { OperationClaims.ProjectOwner };

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectResponse>
    {
        private readonly IProjectService _projectService;
        private readonly ProjectBusinessRules _projectBusinessRules;
        private readonly IMapper _mapper;

        public DeleteProjectCommandHandler(IProjectService projectService, IMapper mapper, ProjectBusinessRules projectBusinessRules)
        {
            _projectService = projectService;
            _projectBusinessRules = projectBusinessRules;
            _mapper = mapper;
        }

        public async Task<DeleteProjectResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            Project? project = await _projectService.GetAsync(x => x.Id == request.ProjectId, enableTracking: false, cancellationToken: cancellationToken);

            await _projectBusinessRules.ProjectShouldBeExistWhenDeleted(project);

            Project result = await _projectService.DeleteAsync(project);

            DeleteProjectResponse response = _mapper.Map<DeleteProjectResponse>(result);

            return response;
        }
    }
}
