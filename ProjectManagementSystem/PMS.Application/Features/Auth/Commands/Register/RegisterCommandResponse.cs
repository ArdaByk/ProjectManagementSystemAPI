using PMS.Core.Security.JWT;

namespace PMS.Application.Features.Auth.Commands.Register;

public class RegisterCommandResponse
{
    public Guid Id { get; set; }
    public AccessToken AccessToken { get; set; }
}