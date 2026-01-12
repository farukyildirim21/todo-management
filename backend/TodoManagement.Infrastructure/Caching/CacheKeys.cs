namespace TodoManagement.Infrastructure.Caching;

public static class CacheKeys
{
    public static string TodoById(Guid id)
        => $"todo:{id}";

    public static string TodosByUser(Guid userId)
        => $"todos:user:{userId}";
}
