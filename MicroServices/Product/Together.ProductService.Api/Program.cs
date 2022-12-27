using Microsoft.IdentityModel.Tokens;
using Together.Product.Process.Implement;
using Together.Product.Process.Interface;
using Together.ProductService.Infrastructure;
using ApiAnchor = Together.ProductService.Api.V1.Anchor;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddCoreServices(builder.Configuration, builder.Environment, typeof(ApiAnchor));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRabitMQProducer, RabitMQProducer>();

builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5069";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
