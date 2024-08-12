using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();
app.UseApiApplication();
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
}

//app.MapGet("/", () => "Hello World!");

app.Run();
