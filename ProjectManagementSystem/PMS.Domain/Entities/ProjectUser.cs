using PMS.Core.Persistence.Repositories;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities;

public class ProjectUser: Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ProjectId { get; set; }
    public string OperationClaim { get; set; }

    public virtual User User { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;

    public ProjectUser(Guid id, Guid userId, Guid projectId, Guid operationClaimId, string operationClaim) :base(id)
    {
        UserId = userId;
        ProjectId = projectId;
        OperationClaim = operationClaim;
    }
    public ProjectUser()
    {
    }
}
