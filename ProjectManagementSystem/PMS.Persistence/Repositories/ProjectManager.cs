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

public class ProjectManager : EfRepositoryBase<Project, Guid, BaseDbContext>, IProjectService
{
    public ProjectManager(BaseDbContext dbContext) : base(dbContext)
    {
    }
}
