using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Text;

namespace TodoManagement.Infrastructure.Identity;

public sealed class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly BasicAuthOptions _options;
    private readonly ILogger<BasicAuthMiddleware> _logger;

    public BasicAuthMiddleware(
        RequestDelegate next,
        IOptions<BasicAuthOptions> options,
        ILogger<BasicAuthMiddleware> logger)
    {
        _next = next;
        _options = options.Value;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, CurrentUser currentUser)
    {
        _logger.LogInformation("➡️ Incoming request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);
       
        if (context.Request.Path.StartsWithSegments("/health"))
        {
            await _next(context);
            return;
        }


        

        if (!context.Request.Headers.TryGetValue("Authorization", out var header))
        {
            _logger.LogWarning("❌ Authorization header MISSING");
            context.Response.StatusCode = 401;
            return;
        }

        _logger.LogInformation("✅ Authorization header PRESENT: {Header}", header.ToString());

        if (!header.ToString().StartsWith("Basic "))
        {
            _logger.LogWarning("❌ Authorization header NOT Basic");
            context.Response.StatusCode = 401;
            return;
        }

        var encoded = header.ToString()["Basic ".Length..];
        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));

        _logger.LogInformation("🔓 Decoded credentials: {Decoded}", decoded);

        var parts = decoded.Split(':');

        if (parts.Length != 2)
        {
            _logger.LogWarning("❌ Invalid credential format");
            context.Response.StatusCode = 401;
            return;
        }

        var username = parts[0];
        var password = parts[1];

        var user = _options.Users.FirstOrDefault(u =>
            u.Username == username && u.Password == password);

        if (user is null)
        {
            _logger.LogWarning("❌ User NOT FOUND: {Username}", username);
            context.Response.StatusCode = 401;
            return;
        }

        _logger.LogInformation("✅ Authenticated user: {Username} ({UserId})",
            username, user.UserId);

        currentUser.Id = user.UserId;

        await _next(context);
    }
}
