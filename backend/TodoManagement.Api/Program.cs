using TodoManagement.Api.Middlewares;
using TodoManagement.Application;
using TodoManagement.Infrastructure;
using TodoManagement.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Application
builder.Services.AddApplication();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Global Exception Handling
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<BasicAuthMiddleware>();

// Routing
app.MapControllers();

app.Run();
