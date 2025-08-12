namespace PMS.Application.Features.Users.Queries.GetList;

public class GetListUserQueryItemDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool EmailVerified { get; set; }
}