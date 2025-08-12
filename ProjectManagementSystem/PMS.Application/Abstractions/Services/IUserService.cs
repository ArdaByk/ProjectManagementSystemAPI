using PMS.Core.Persistence.Repositories;
using PMS.Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Abstractions.Services;

public interface IUserService: IAsyncRepository<User, Guid>, IRepository<User, Guid>
{
}
