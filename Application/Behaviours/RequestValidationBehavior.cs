using Application.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

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
                    //var propertyName = failureGroup.Key;
                    var propertyFailures = failureGroup.ToArray();

                    errors.Add($"{string.Join(", ",propertyFailures)}");
                }
                var result =  Result.Failure(errors) as TResponse;

                return result!;
            }

            return await next();
        }
    }
}