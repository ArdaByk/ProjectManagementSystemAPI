using PMS.Application.Abstractions.Services;
using PMS.Core.Persistence.Repositories;
using PMS.Domain.Entities;
using PMS.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.Repositories;

public class ProjectTaskUserManager : EfRepositoryBase<ProjectTaskUser, Guid, BaseDbContext>, IProjectTaskUserService
{
    public ProjectTaskUserManager(BaseDbContext dbContext) : base(dbContext)
    {
    }
}
