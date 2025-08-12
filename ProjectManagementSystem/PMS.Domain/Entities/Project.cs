using PMS.Core.Persistence.Repositories;
using PMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PMS.Domain.Entities;

public class Project : Entity<Guid>
{
    public Project(Guid id, string projectName, string? projectDescription, ProjectStatus status, int memberCount)
    : base(id)
    {
        ProjectName = projectName;
        ProjectDescription = projectDescription;
        Status = status;
        MemberCount = memberCount;
        ProjectTasks = new HashSet<ProjectTaskUser>();
    }

    public Project()
    {
        ProjectTasks = new HashSet<ProjectTaskUser>();
    }
    
    public string ProjectName { get; set; }
    public string? ProjectDescription { get; set; }
    public ProjectStatus Status { get; set; }
    public int MemberCount { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProjectTaskUser>? ProjectTasks { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
}
