using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
    public Task HandleExceptionAsync(Exception exception)
    {
        switch (exception)
        {
            case BusinessException businessException:
                return HandleException(businessException);
            case ValidationException validationException:
                return HandleException(validationException);
            case AuthorizationException authorizationException:
                return HandleException(authorizationException);
            default:
                return HandleException(exception);
        }
    }

    protected abstract Task HandleException(Exception exception);
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(BusinessException businessException);
    protected abstract Task HandleException(AuthorizationException authorizationException);
}
