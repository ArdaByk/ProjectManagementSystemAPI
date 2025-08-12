using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.ProjectUsers.Commands.LeaveProject;

namespace PMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectUsersController : BaseController
{
    [HttpGet("leave")]
    public async Task<IActionResult> LeaveProject([FromQuery] LeaveProjectCommand leaveProjectCommand)
    {
        LeaveProjectCommandResponse response = await Mediator.Send(leaveProjectCommand);

        return Ok(response);
    }
}
