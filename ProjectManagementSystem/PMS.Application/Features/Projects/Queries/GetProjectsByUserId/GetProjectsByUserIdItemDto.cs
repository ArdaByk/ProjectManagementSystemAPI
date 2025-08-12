using PMS.Domain.Enums;

namespace PMS.Application.Features.Projects.Queries.GetProjectsByUserId;

public class GetProjectsByUserIdItemDto
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public ProjectStatus Status { get; set; }
    public int MemberCount { get; set; }
}