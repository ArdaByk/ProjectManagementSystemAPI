using PMS.Application.Abstractions.Services;
using PMS.Domain.Entities;
using PMS.Persistence.Contexts;
using PMS.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Persistence.Repositories;

public class ProjectTaskManager : EfRepositoryBase<ProjectTask, Guid, BaseDbContext>, IProjectTaskService
{
    public ProjectTaskManager(BaseDbContext dbContext) : base(dbContext)
    {
    }
}
