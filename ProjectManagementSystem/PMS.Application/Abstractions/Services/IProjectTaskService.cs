using PMS.Core.Persistence.Repositories;
using PMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Abstractions.Services;

public interface IProjectTaskService : IAsyncRepository<ProjectTask, Guid>, IRepository<ProjectTask, Guid>
{
}
