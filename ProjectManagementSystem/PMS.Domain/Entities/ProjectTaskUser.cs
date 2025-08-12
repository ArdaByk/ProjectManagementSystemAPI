using PMS.Core.Persistence.Repositories;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PMS.Domain.Entities;

public class ProjectTaskUser : Entity<Guid>
{
    public ProjectTaskUser(Guid userId, Guid projectTaskId, Guid projectId)
    {
        UserId = userId;
        ProjectTaskId = projectTaskId;
        ProjectId = projectId;
    }
    public ProjectTaskUser()
    {
        
    }

    public Guid UserId { get; set; }
    public Guid ProjectTaskId { get; set; }
    public Guid ProjectId { get; set; }

    [JsonIgnore]
    public virtual Project Project { get; set; } = null!;
    [JsonIgnore]
    public virtual User User { get; set; } = null!;
    [JsonIgnore]
    public virtual ProjectTask ProjectTask { get; set; } = null!;
}
