using Integrativa.Application;
using Integrativa.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? throw new InvalidOperationException("ConnectionStrings:Default não configurada.");

builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();