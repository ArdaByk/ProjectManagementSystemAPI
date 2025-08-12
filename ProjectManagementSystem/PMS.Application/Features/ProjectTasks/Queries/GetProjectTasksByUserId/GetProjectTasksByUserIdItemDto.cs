using PMS.Domain.Entities;
using PMS.Domain.Enums;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByUserId;

public class GetProjectTasksByUserIdItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
}