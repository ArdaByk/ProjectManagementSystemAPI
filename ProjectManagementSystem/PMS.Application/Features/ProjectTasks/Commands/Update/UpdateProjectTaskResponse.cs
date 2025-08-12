using PMS.Domain.Enums;

namespace PMS.Application.Features.ProjectTasks.Commands.Update;

public class UpdateProjectTaskResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
}