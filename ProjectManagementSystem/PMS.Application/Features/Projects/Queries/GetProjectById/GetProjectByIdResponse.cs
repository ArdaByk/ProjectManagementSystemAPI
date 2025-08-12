using PMS.Domain.Enums;

namespace PMS.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public ProjectStatus Status { get; set; }
    public int MemberCount { get; set; }
}