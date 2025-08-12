using PMS.Core.Security.JWT;

namespace PMS.Application.Features.Auth.Commands.Login;

public class LoginCommandResponse
{
    public Guid UserId { get; set; }
    public AccessToken AccessToken { get; set; }
}