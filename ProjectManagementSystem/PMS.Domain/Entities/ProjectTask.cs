using PMS.Core.Persistence.Repositories;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities;

public class ProjectTask : Entity<Guid>
{
    public ProjectTask(Guid id, string title, string content, ProjectTaskStatus status, Guid projectId, Guid userId) : base(id)
    {
        Title = title;
        Content = content;
        Status = status;
        ProjectId = projectId;
        Users = new List<ProjectTaskUser>();
    }
    public ProjectTask()
    {
        Users = new List<ProjectTaskUser>();
    }
    public string Title { get; set; }
    public string Content { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public Guid ProjectId { get; set; }

    public virtual ICollection<ProjectTaskUser> Users { get; set; }
    public virtual Project Project { get; set; } = null!;

}
