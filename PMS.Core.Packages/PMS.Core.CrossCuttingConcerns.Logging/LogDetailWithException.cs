using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.CrossCuttingConcerns.Logging;

public class LogDetailWithException : LogDetail
{
    public string ErrorMessage { get; set; }
    public LogDetailWithException()
    {
        ErrorMessage = string.Empty;
    }

    public LogDetailWithException(string methodName, string fullName, string user, List<LogParameter> parameters, string errorMessage) : base(methodName, fullName, user, parameters)
    {
        ErrorMessage = errorMessage;
    }
}
