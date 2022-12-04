using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Together.Core.Repository;
using Together.CustomerService.Core.Entities;
using Together.CustomerService.Infrastructor.Data;
using Together.CustomerService.Infrastructor.Repositories;
using Together.Infrastructure;
using Together.Infrastructure.Swagger;
using Together.Infrastructure.Validator;
using AppCoreAnchor = Together.CustomerService.Core.Anchor;
namespace Together.CustomerService.Infrastructor
{
    public static class Extensions
    {
        private const string CorsName = "api";

        public static IServiceCollection AddCoreServices(this IServiceCollection services,
            IConfiguration config, IWebHostEnvironment env, Type apiType)
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
            services.AddSwagger(apiType);

            services.AddDbContext<CustomerDbContext>(
                (options => options.UseSqlServer(config.GetConnectionString("defaultConnection"))));
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
            return app.UseSwagger(provider);
        }
    }
}
