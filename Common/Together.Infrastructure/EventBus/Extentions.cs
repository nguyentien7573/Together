using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Together.Core.Domain;

namespace Together.Infrastructure.EventBus
{
    public static class Extentions
    {
        [Obsolete]
        public static IServiceCollection AddMassTransit(this IServiceCollection services, string url, string userName, string password)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(url, "/", h =>
                    {
                        h.Username(userName);
                        h.Password(password);
                    });

                });

            });


            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
            services.AddMassTransitHostedService();

            return services;
        }

        public static IApplicationBuilder UseMassTransit(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            return app;
        }
    }
}
