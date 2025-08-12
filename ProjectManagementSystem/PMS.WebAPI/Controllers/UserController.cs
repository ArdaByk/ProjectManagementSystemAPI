using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Features.Users.Commands.Delete;
using PMS.Application.Features.Users.Commands.Update;
using PMS.Application.Features.Users.Queries.GetById;
using PMS.Application.Features.Users.Queries.GetList;
using PMS.Core.Application.Requests;
using PMS.Core.Application.Responses;

namespace PMS.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : BaseController
{
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUserCommand)
    {
        DeleteUserResponse response = await Mediator.Send(deleteUserCommand);

        return Ok(response);
    }
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
    {
        UpdateUserCommandResponse response = await Mediator.Send(updateUserCommand);

        return Ok(response);
    }
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserQuery getByIdUserQuery)
    {
        GetByIdUserResponse response = await Mediator.Send(getByIdUserQuery);

        return Ok(response);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery] PageRequest pageRequest)
    {
        GetListUserQuery getListUserQuery = new GetListUserQuery { PageRequest = pageRequest };

        GetListResponse<GetListUserQueryItemDto> response = await Mediator.Send(getListUserQuery);

        return Ok(response);
    }
}
