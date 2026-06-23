using System.Collections.Immutable;
using ArcTestsData.Entities;
using ArcTestsData.Interfaces;
using ArcTestsData.Repositories;
using ArcTestsServices.Interfaces;
using ArcTestsServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DI wiring, the only place where ArcTestsData.Repositories is referenced from the API project 
builder.Services.AddScoped<IRepository<ProductEntity>, ProductRepository>();
builder.Services.AddScoped<IRepository<OrderEntity>, OrderRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program{}