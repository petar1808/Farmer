using Application.Features.Identity.Commands.Login;
using Application.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection;

namespace Application.Behaviours
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => this.validators = validators;

        private readonly IEnumerable<IValidator<TRequest>> validators;

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = this
                .validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                var errors = new List<string>();
                var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

                foreach (var failureGroup in failureGroups)
                {
                    var propertyFailures = failureGroup.ToArray();

                    errors.Add($"{string.Join(", ",propertyFailures)}");
                }

                if (typeof(TResponse) == typeof(Result))
                {
                    var result = Result.Failure(errors);
                    return (TResponse)(object)result;
                }
                else if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                {
                    Type resultGenericType = typeof(Result<>);
                    Type typeArguments = typeof(TResponse).GetGenericArguments()[0];
                    Type resultType = resultGenericType.MakeGenericType(typeArguments);

                    object resultInstance = Activator.CreateInstance(resultType, false, null, errors)!;

                    return (TResponse)resultInstance;
                }
                else
                {
                    throw new InvalidOperationException($"Invalid response type: {typeof(TResponse)}");
                }
            }

            return await next();
        }
    }
}