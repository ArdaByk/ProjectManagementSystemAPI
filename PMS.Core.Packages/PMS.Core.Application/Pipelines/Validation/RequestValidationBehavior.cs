using FluentValidation;
using MediatR;
using PMS.Core.CrossCuttingConcerns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = PMS.Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace PMS.Core.Application.Pipelines.Validation;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<object> context = new(request);

        IEnumerable<ValidationExceptionModel> errors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(e => e.Errors)
            .Where(f => f != null)
            .GroupBy(
                keySelector: p => p.PropertyName,
                resultSelector: (propertyName, errors) =>
                new ValidationExceptionModel
                {
                    Property = propertyName,
                    Errors = errors.Select(x => x.ErrorMessage).ToArray()
                }
            ).ToList();

        if (errors.Any())
            throw new ValidationException(errors);
        TResponse response = await next();
        return response;
            
    }
}
