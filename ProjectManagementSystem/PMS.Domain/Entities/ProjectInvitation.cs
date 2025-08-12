using PMS.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Entities;

public class ProjectInvitation : Entity<Guid>
{
    public ProjectInvitation(Guid projectId, Guid userId, DateTime expiredDate)
    {
        ProjectId = projectId;
        UserId = userId;
        ExpiredDate = expiredDate;
    }
    public ProjectInvitation()
    {
        
    }

    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiredDate { get; set; }
}
