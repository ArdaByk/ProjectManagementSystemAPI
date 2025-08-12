using PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsers;
using PMS.Domain.Enums;

namespace PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsersQuery;

public class GetProjectWithProjectTasksAndUsersResponse
{
    public Guid Id { get; set; }
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public ProjectStatus Status { get; set; }
    public int MemberCount { get; set; }

    public ICollection<ProjectTaskItemDto> ProjectTasks { get; set; }
    public ICollection<ProjectUserListItemDto> ProjectUsers { get; set; }
}