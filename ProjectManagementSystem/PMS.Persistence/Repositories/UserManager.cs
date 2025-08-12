using PMS.Application.Abstractions.Services;
using PMS.Core.Persistence.Repositories;
using PMS.Core.Security.Entities;
using PMS.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.Repositories;

public class UserManager : EfRepositoryBase<User, Guid, BaseDbContext>, IUserService
{
    public UserManager(BaseDbContext dbContext) : base(dbContext)
    {
    }
}
