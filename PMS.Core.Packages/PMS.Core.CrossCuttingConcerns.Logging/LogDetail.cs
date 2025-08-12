using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.CrossCuttingConcerns.Logging;

public class LogDetail
{
    public string MethodName { get; set; }
    public string FullName { get; set; }
    public string User { get; set; }
    public List<LogParameter> Parameters { get; set; }
    public LogDetail()
    {
        MethodName = string.Empty;
        FullName = string.Empty;
        User = string.Empty;
        Parameters = new List<LogParameter>();
    }

    public LogDetail(string methodName, string fullName, string user, List<LogParameter> parameters)
    {
        MethodName = methodName;
        FullName = fullName;
        User = user;
        Parameters = parameters;
    }
}
