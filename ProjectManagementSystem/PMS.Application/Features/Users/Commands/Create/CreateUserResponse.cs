namespace PMS.Application.Features.Users.Commands.Create;

public class CreateUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
}