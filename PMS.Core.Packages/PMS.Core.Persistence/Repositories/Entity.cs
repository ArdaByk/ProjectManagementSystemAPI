using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Persistence.Repositories;

public class Entity<TId> : IEntityTimestamp
{
    public TId Id { get; set; }

    public Entity(TId id)
    {
        Id = id;
    }
    public Entity()
    {
        Id = default;
    }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}
