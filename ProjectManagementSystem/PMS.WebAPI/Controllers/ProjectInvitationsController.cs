using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.ProjectInvitations.Commands.Send;
using PMS.Application.Features.ProjectInvitations.Commands.Accept;

namespace PMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectInvitationsController : BaseController
{
    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] SendProjectInvitationCommand sendProjectInvitationCommand)
    {
        SendProjectInvitationResponse response = await Mediator.Send(sendProjectInvitationCommand);

        return Ok(response);
    }
    [HttpGet("accept/{invitationId}")]
    public async Task<IActionResult> Accept([FromRoute] Guid invitationId)
    {
        AcceptProjectInvitationCommand command = new AcceptProjectInvitationCommand { Id = invitationId };

        AcceptProjectInvitationResponse response = await Mediator.Send(command);

        return Ok(response);
    }
}
