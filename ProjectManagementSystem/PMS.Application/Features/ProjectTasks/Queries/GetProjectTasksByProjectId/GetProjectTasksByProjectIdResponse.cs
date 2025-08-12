using PMS.Domain.Enums;

namespace PMS.Application.Features.ProjectTasks.Queries.GetProjectTasksByProjectId;

public class GetProjectTasksByProjectIdResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }

    public ICollection<ProjectTaskUserDto> Users { get; set; }
}