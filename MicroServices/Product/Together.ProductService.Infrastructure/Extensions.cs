﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Together.Core.Repository;
using Together.Infrastructure.Validator;
using Together.Infrastructure;
using Together.Infrastructure.Swagger;
using Together.ProductService.Core.Entities;
using Together.ProductService.Infrastructure.Data;
using Together.ProductService.Infrastructure.Repositories;
using AppCoreAnchor = Together.ProductService.Core.Anchor;
using Together.ProductService.Core.Interface;

namespace Together.ProductService.Infrastructure
{
    public static class Extensions
    {
        private const string CorsName = "api";

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

            services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(config.GetConnectionString("defaultConnection")));

            services.AddScoped<IProductRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<Category>, CategoryRepository>();

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
