namespace TodoManagement.Infrastructure.Identity;

public sealed class BasicAuthOptions
{
    public List<BasicAuthUser> Users { get; init; } = new();
}

public sealed class BasicAuthUser
{
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
    public Guid UserId { get; init; }
}