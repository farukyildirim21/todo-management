using TodoManagement.Api.Middlewares;
using TodoManagement.Application;
using TodoManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Controllers
builder.Services.AddControllers();

// 🔹 Application
builder.Services.AddApplication();

// 🔹 Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// 🔹 Global Exception Handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// 🔹 Routing
app.MapControllers();

app.Run();
