using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.CrossCuttingConcerns.Exceptions.Types;

public class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; }

    public ValidationException(string message):base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }
    public ValidationException(string message, Exception innerException):base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }
    public ValidationException(IEnumerable<ValidationExceptionModel> errors):base(BuildErrorMessages(errors))
    {
        Errors = errors;
    }

    private static string BuildErrorMessages(IEnumerable<ValidationExceptionModel> errors)
    {
        IEnumerable<string> errs = errors.Select(e => $"{Environment.NewLine} -- {e.Property}: {string.Join(Environment.NewLine, values: e.Errors ?? Array.Empty<string>())}");
        return "Validation Failed: " + string.Join(string.Empty, errs);
    }
}

public class ValidationExceptionModel
{
    public string? Property { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}