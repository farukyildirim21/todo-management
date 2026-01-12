namespace TodoManagement.Api.Contracts.Responses;

public class TodoDetailResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Status { get; set; } = null!;
}
