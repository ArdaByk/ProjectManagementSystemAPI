using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Application.Pipelines.Caching;

public class CacheSettings
{
    public int SlidingExpiration { get; set; }
}
