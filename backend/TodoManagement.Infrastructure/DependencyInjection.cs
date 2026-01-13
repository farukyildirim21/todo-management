using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using StackExchange.Redis;
using TodoManagement.Application.Abstractions.Caching;
using TodoManagement.Application.Abstractions.Messaging;
using TodoManagement.Application.Abstractions.Persistence;
using TodoManagement.Infrastructure.Caching;
using TodoManagement.Infrastructure.Events;
using TodoManagement.Infrastructure.Persistence;
using TodoManagement.Domain.Todos.Events;
using TodoManagement.Infrastructure.Events.Handlers;


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
            var connectionString = configuration.GetConnectionString("MongoDb");
            return new MongoClient(connectionString);
        });

        services.AddSingleton<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var databaseName = configuration.GetValue<string>("MongoDb:Database");
            return client.GetDatabase(databaseName);
        });

        // 🔹 Redis
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redisConnection = configuration.GetConnectionString("Redis")
    ?? throw new InvalidOperationException("Redis connection string is missing");

return ConnectionMultiplexer.Connect(redisConnection);
        });

        services.AddScoped<ICacheService, RedisCacheService>();

        // 🔹 Repositories
        services.AddScoped<ITodoRepository, MongoTodoRepository>();
        services.AddScoped<ITodoReadRepository, MongoTodoReadRepository>();

        // 🔹 Domain Events
        services.AddScoped<IDomainEventDispatcher, InMemoryDomainEventDispatcher>();

        services.AddScoped<IDomainEventHandler<TodoCompletedDomainEvent>,
            TodoCompletedCacheInvalidationHandler>();

        services.AddScoped<IDomainEventHandler<TodoCancelledDomainEvent>,
            TodoCancelledCacheInvalidationHandler>();

        return services;
    }
}
