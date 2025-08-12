using PMS.Domain.Enums;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksWithUsersByProjectId;

public class GetProjectTasksWithUsersByProjectIdItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
    public ICollection<UserItemDto> Users { get; set; }
}