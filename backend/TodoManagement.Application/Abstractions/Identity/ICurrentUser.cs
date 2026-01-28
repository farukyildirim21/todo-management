namespace TodoManagement.Application.Abstractions.Identity;

public interface ICurrentUser
{
    Guid Id { get; }
}