namespace PMS.Application.Features.Auth.Queries.VerifyEmail;

public class VerifyEmailResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
}