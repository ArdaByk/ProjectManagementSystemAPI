namespace PMS.Application.Features.Users.Commands.Update;

public class UpdateUserCommandResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}