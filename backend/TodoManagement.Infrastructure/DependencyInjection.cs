using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using TodoManagement.Application.Abstractions.Caching;
using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Application.Abstractions.Identity;
using TodoManagement.Infrastructure.Caching;
using TodoManagement.Infrastructure.Events;
using TodoManagement.Infrastructure.Persistence;
using TodoManagement.Domain.Todos.Events;
using TodoManagement.Infrastructure.Events.Handlers;
using TodoManagement.Infrastructure.Identity;


namespace TodoManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
      
       // 🔹 MongoDB
services.AddSingleton<IMongoClient>(_ =>
{
    var connectionString = configuration["MongoDb:ConnectionString"]
        ?? throw new InvalidOperationException("MongoDb connection string missing");

    return new MongoClient(connectionString);
});

services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = configuration["MongoDb:Database"]
        ?? throw new InvalidOperationException("MongoDb database name missing");

    return client.GetDatabase(databaseName);
});


        // 🔹 Redis
        services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisConnection = configuration["Redis:ConnectionString"]
    ?? throw new InvalidOperationException("Redis connection string is missing");


    var options = ConfigurationOptions.Parse(redisConnection);

    // 🔥 EN KRİTİK SATIR
    options.AbortOnConnectFail = false;

    // Tavsiye edilen ek ayarlar
    options.ConnectRetry = 5;
    options.ConnectTimeout = 5000;
    options.ReconnectRetryPolicy = new ExponentialRetry(1000);

    return ConnectionMultiplexer.Connect(options);
});


        services.AddScoped<ICacheService, RedisCacheService>();

        // Repositories
        services.AddScoped<ITodoRepository, MongoTodoRepository>();
        services.AddScoped<ITodoReadRepository, MongoTodoReadRepository>();

        // Domain Events
        services.AddScoped<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();

        services.AddScoped<IDomainEventHandler<TodoCompletedDomainEvent>,
            TodoCompletedCacheInvalidationHandler>();

        services.AddScoped<IDomainEventHandler<TodoCancelledDomainEvent>,
            TodoCancelledCacheInvalidationHandler>();
        services.AddScoped<
            IDomainEventHandler<TodoCreatedDomainEvent>,
            TodoCreatedCacheInvalidationHandler>();
        
        services.Configure<BasicAuthOptions>(
            configuration.GetSection("BasicAuth"));

        services.AddScoped<CurrentUser>();
        services.AddScoped<ICurrentUser>(sp =>
            sp.GetRequiredService<CurrentUser>());
        return services;
    }
}
