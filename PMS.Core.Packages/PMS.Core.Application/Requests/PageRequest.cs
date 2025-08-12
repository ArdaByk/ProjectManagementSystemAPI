using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Application.Requests;

public class PageRequest
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}