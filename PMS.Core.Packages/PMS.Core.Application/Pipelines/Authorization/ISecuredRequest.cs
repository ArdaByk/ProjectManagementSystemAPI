using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Application.Pipelines.Authorization;

public interface ISecuredRequest
{
    Guid ProjectId { get; }
    ICollection<string> RequiredProjectRoles { get; }
}
