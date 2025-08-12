using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Security.Authorization;

public interface IProjectRoleProvider
{
    Task<string>? GetUserOperationClaim(Guid? userId, Guid projectId);
}
