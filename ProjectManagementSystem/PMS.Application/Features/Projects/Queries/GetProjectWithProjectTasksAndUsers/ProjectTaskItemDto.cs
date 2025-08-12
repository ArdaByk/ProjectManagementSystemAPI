using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Features.Projects.Queries.GetProjectWithProjectTasksAndUsers;

public class ProjectTaskItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectStatus Status { get; set; }
    public ICollection<ProjectTaskUserItemDto> Users { get; set; }
}
