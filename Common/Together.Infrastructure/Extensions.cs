using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Together.Infrastructure.Validator;

namespace Together.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomMediatR(this IServiceCollection services, Type[] types = null,
           Action<IServiceCollection> doMoreActions = null)
        {
            services.AddHttpContextAccessor();

            services.AddMediatR(types)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            doMoreActions?.Invoke(services);

            return services;
        }
    }
}
