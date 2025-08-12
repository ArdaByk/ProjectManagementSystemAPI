using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.ProjectTasks.Commands.Create;
using PMS.Application.Features.ProjectTasks.Commands.Delete;
using PMS.Application.Features.ProjectTasks.Commands.Update;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByUserId;
using PMS.Application.Features.ProjectTasks.Queries.GetAProjectsTasksByUserId;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksWithUsersByProjectId;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTaskById;
using PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByProjectId;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;

namespace PMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectTasksController : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProjectTaskCommand createProjectTaskCommand)
    {
        CreateProjectTaskResponse response = await Mediator.Send(createProjectTaskCommand);

        return Ok(response);
    }
    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateProjectTaskCommand updateProjectTaskCommand)
    {
        UpdateProjectTaskResponse response = await Mediator.Send(updateProjectTaskCommand);

        return Ok(response);
    }
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteProjectTaskCommand deleteProjectTaskCommand)
    {
        DeleteProjectTaskResponse response = await Mediator.Send(deleteProjectTaskCommand);

        return Ok(response);
    }
    [HttpGet("get/{projectId}/{projectTaskId}")]
    public async Task<IActionResult> GetTaskById([FromRoute] GetProjectTaskByIdQuery getByIdProjectTaskQuery)
    {
        GetProjectTaskByIdResponse response = await Mediator.Send(getByIdProjectTaskQuery);

        return Ok(response);
        
    }
    [HttpGet("project")]
    public async Task<IActionResult> GetProjectTasksByProjectId([FromQuery] PageRequest pageRequest, [FromQuery] Guid projectId)
    {
        GetProjectTasksByProjectIdQuery getByIdProjectTaskQuery = new GetProjectTasksByProjectIdQuery { ProjectId = projectId, PageRequest = pageRequest };

       GetListResponse<GetProjectTasksByProjectIdResponse> response = await Mediator.Send(getByIdProjectTaskQuery);

        return Ok(response);
    }
    [HttpGet("tasksbyuserid")]
    public async Task<IActionResult> GetTasksByUserId([FromQuery] PageRequest pageRequest, [FromQuery] Guid userId)
    {
        GetProjectTasksByUserIdQuery getListByUserIdQuery = new GetProjectTasksByUserIdQuery { PageRequest = pageRequest,UserId=userId};

        GetListResponse<GetProjectTasksByUserIdItemDto> response = await Mediator.Send(getListByUserIdQuery);

        return Ok(response);
    }
    [HttpGet("tasks/byuserid")]
    public async Task<IActionResult> GetAProjectsTasksByUserId([FromQuery] Guid userId, [FromQuery] Guid projectId, [FromQuery] PageRequest pageRequest)
    {
        GetAProjectsTasksByUserIdQuery getListProjectTasksForAProjectByUserIdQuery = new GetAProjectsTasksByUserIdQuery { PageRequest = pageRequest,ProjectId=projectId,UserId=userId};

        GetListResponse<GetAProjectsTasksByUserIdItemDto> response = await Mediator.Send(getListProjectTasksForAProjectByUserIdQuery);

        return Ok(response);
    }
    [HttpGet("tasks")]
    public async Task<IActionResult> GetProjectTasksWithUsersByProjectId([FromQuery] Guid projectId, [FromQuery] PageRequest pageRequest)
    {
        GetProjectTasksWithUsersByProjectIdQuery getListTasksWithUsersByProjectIdQuery = new GetProjectTasksWithUsersByProjectIdQuery { PageRequest = pageRequest, ProjectId = projectId };

        GetListResponse<GetProjectTasksWithUsersByProjectIdItemDto> response = await Mediator.Send(getListTasksWithUsersByProjectIdQuery);

        return Ok(response);
    }
}
