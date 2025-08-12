namespace PMS.Application.Features.Projects.Commands.Update;

public class UpdateProjectResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public int MemberCount { get; set; }
}