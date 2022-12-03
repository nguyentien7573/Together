using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Together.Infrastructure.Validator
{
    public static class Extensions
    {
        private static ValidationResultModel ToValidationResultModel(this ValidationResult validationResult)
        {
            return new ValidationResultModel(validationResult);
        }

        public static async Task HandleValidation<TRequest>(this IValidator<TRequest> validator, TRequest request)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToValidationResultModel());
            }
        }

        public static IServiceCollection AddCustomValidators(this IServiceCollection services, Type[] types)
        {
            return services.Scan(scan => scan
                .FromAssembliesOf(types)
                .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
