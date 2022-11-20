using Microsoft.EntityFrameworkCore;
using Together.Products.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var config =  builder.Configuration;

builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer("Server=NGUYENTIEN;Database=Together.Product;Trusted_Connection=True;TrustServerCertificate=True;"));
   
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
