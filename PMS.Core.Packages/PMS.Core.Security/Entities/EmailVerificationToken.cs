using PMS.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.Entities;

public class EmailVerificationToken : Entity<Guid>
{
    public EmailVerificationToken(Guid userId, DateTime expireTime)
    {
        UserId = userId;
        ExpireTime = expireTime;
    }

    public EmailVerificationToken(Guid id, Guid userId, DateTime expireTime):base(id)
    {
        UserId = userId;
        ExpireTime = expireTime;
    }
    public EmailVerificationToken()
    {
        UserId = default!;
    }
    public Guid UserId { get; set; }
    public DateTime ExpireTime { get; set; }

    public virtual User User { get; set; } = null!;

}
