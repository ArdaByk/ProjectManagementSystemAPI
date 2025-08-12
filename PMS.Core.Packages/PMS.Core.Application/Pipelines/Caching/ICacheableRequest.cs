using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Application.Pipelines.Caching;

public interface ICacheableRequest
{
   bool BypassCache { get; }
   string CacheKey { get; }
   TimeSpan? SlidingExpiration { get; }
   string? CacheGroupKey { get; }
}