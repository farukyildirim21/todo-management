using TodoManagement.Api.Middlewares;
using TodoManagement.Application;
using TodoManagement.Infrastructure;
using TodoManagement.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Explicitly bind Kestrel to all interfaces on 8080 for container access
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

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
