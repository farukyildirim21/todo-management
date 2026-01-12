namespace TodoManagement.Api.Contracts.Requests;

public class CreateTodoRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = null!;
}
