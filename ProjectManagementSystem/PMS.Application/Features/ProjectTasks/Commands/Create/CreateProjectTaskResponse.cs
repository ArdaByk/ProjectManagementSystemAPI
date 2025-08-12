using PMS.Domain.Enums;

namespace PMS.Application.Features.ProjectTasks.Commands.Create;

public class CreateProjectTaskResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }
}