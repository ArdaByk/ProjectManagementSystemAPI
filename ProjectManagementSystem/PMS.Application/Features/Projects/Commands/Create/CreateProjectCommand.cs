using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using PMS.Application.Abstractions.Services;
using PMS.Core.Application.Pipelines.Caching;
using PMS.Core.Application.Pipelines.Logging;
using PMS.Core.Security.Extensions;
using PMS.Domain.Entities;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Commands.Create;

public class CreateProjectCommand : IRequest<CreateProjectResponse>, ILoggableRequest
{
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
    {
        private readonly IProjectService _projectService;
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IProjectService projectService, IProjectUserService projectUserService ,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _projectService = projectService;
            _projectUserService = projectUserService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<CreateProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.GetIdClaim());

            Project project = _mapper.Map<Project>(request);
            project.Status = ProjectStatus.NotStarted;

            var result = await _projectService.AddAsync(project);

            ProjectUser projectUser = new ProjectUser {OperationClaim = OperationClaims.ProjectOwner, ProjectId = project.Id, UserId = userId };
            await _projectUserService.AddAsync(projectUser);

            CreateProjectResponse response = _mapper.Map<CreateProjectResponse>(result);

            return response;
        }
    }
}
