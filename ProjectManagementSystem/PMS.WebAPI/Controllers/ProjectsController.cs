using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Projects.Commands.Create;
using PMS.Application.Features.Projects.Commands.Delete;
using PMS.Application.Features.Projects.Commands.Update;
using PMS.Application.Features.Projects.Queries.GetProjectById;
using PMS.Application.Features.Projects.Queries.GetOwnedProjectsByUserId;
using PMS.Application.Features.Projects.Queries.GetProjectsByUserId;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;
using PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsers;
using PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsersQuery;

namespace PMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : BaseController
{
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProjectCommand createProjectCommand)
    {
        CreateProjectResponse response = await Mediator.Send(createProjectCommand);

        return Ok(response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProjectCommand updateProjectCommand)
    {
        UpdateProjectResponse response = await Mediator.Send(updateProjectCommand);

        return Ok(response);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProjectCommand deleteProjectCommand)
    {
        DeleteProjectResponse response = await Mediator.Send(deleteProjectCommand);

        return Ok(response);
    }
    [HttpGet("get/{ProjectId}")]
    public async Task<IActionResult> GetById([FromRoute] GetProjectByIdQuery getByIdProjectQuery)
    {
        GetProjectByIdResponse response = await Mediator.Send(getByIdProjectQuery);

        return Ok(response);
    }

    [HttpGet("get/project/{ProjectId}")]
    public async Task<IActionResult> GetProjectWithTasksAndUsers([FromRoute] GetProjectWithProjectTasksAndUsersQuery getByProjectTasksAndUsersQuery)
    {
        GetProjectWithProjectTasksAndUsersResponse response = await Mediator.Send(getByProjectTasksAndUsersQuery);

        return Ok(response);
    }

    [HttpGet("myownprojects")]
    [Authorize]
    public async Task<IActionResult> GetMyOwnProjects([FromQuery] PageRequest pageRequest)
    {
        GetOwnedProjectsByUserIdQuery getOwnedProjectsByUserIdQuery = new GetOwnedProjectsByUserIdQuery { PageRequest = pageRequest };

        GetListResponse<GetOwnedProjectsByUserIdResponse> response = await Mediator.Send(getOwnedProjectsByUserIdQuery);

        return Ok(response);
    }
    [Authorize]
    [HttpGet("myprojects")]
    public async Task<IActionResult> GetMyProjects([FromQuery] PageRequest pageRequest, [FromQuery] Guid userId)
    {
        GetProjectsByUserIdQuery getProjectsByUserIdQuery = new GetProjectsByUserIdQuery { PageRequest = pageRequest, UserId = userId };

        GetListResponse<GetProjectsByUserIdItemDto> response = await Mediator.Send(getProjectsByUserIdQuery);

        return Ok(response);
    }
}
