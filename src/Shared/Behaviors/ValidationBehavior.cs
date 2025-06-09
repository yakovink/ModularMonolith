 




namespace Shared.Behaviors;

public class ValidationBehavior<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest>? context = new ValidationContext<TRequest>(request);
        ValidationResult[]? validationResults =
            await Task.WhenAll(validators.Select(
                v => v.ValidateAsync(context, cancellationToken)
                ));

        List<ValidationFailure?> failures = validationResults.Where(r => r.Errors.Count > 0)
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }
        return await next();

    }
}
