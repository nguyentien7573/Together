using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Together.Core.Repository;
using Together.Infrastructure;
using Together.Infrastructure.EventBus;
using Together.Infrastructure.Swagger;
using Together.Infrastructure.Validator;
using Together.OrderService.Core.Entities;
using Together.OrderService.Core.EventBus;
using Together.OrderService.Infrastructure.Data;
using Together.OrderService.Infrastructure.Repositories;
using AppCoreAnchor = Together.OrderService.Core.Anchor;

namespace Together.OrderService.Infrastructure
{
    public static class Extensions
    {
        private const string CorsName = "api";

        [Obsolete]
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env, Type apiType)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsName, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddHttpContextAccessor();
            services.AddCustomMediatR(new[] { typeof(AppCoreAnchor) });
            services.AddCustomValidators(new[] { typeof(AppCoreAnchor) });
            services.AddControllers();
            services.AddSwagger(apiType);

            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(config.GetConnectionString("defaultConnection")));

            services.AddScoped<IRepository<Order>, OrderRepository>();
            services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConsumer>(typeof(OrderConsumerDefinition));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config["EventBus:QueueUri"], "/", h =>
                    {
                        h.Username(config["EventBus:UserName"]);
                        h.Password(config["EventBus:PassWord"]);
                    });

                    cfg.ConfigureEndpoints(context);
                });

            });

            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    options.WaitUntilStarted = true;
                    options.StartTimeout = TimeSpan.FromSeconds(10);
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
            services.AddMassTransitHostedService();

            return services;
        }

        public static IApplicationBuilder UseCoreApplication(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsName);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("ApiCaller");
            });

            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}
