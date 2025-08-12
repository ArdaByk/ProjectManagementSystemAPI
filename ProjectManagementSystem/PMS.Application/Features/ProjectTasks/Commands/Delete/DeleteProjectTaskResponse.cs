using PMS.Domain.Enums;

namespace PMS.Application.Features.ProjectTasks.Commands.Delete;

public class DeleteProjectTaskResponse
{
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
}