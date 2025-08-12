using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Auth.Commands.Login;
using PMS.Application.Features.Auth.Commands.Register;
using PMS.Application.Features.Auth.Queries.VerifyEmail;

namespace PMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        RegisterCommandResponse response = await Mediator.Send(registerCommand);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        LoginCommandResponse response = await Mediator.Send(loginCommand);

        return Ok(response);
    }

    [Authorize]
    [HttpGet("verify/{Id}")]
    public async Task<IActionResult> Verify([FromRoute] VerifyEmailQuery verifyEmailCommand)
    {
        VerifyEmailResponse response = await Mediator.Send(verifyEmailCommand);

        return Ok(response);
    }
}
