﻿using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors;

//public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<IRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommand<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(result => result.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
