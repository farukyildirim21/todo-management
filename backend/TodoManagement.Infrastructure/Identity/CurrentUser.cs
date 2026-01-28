using TodoManagement.Application.Abstractions.Identity;

namespace TodoManagement.Infrastructure.Identity;

public sealed class CurrentUser : ICurrentUser
{
    public Guid Id { get; internal set; }
}
