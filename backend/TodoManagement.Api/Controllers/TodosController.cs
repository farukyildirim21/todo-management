using Microsoft.AspNetCore.Mvc;
using TodoManagement.Api.Contracts.Requests;
using TodoManagement.Application.Abstractions.Identity;
using TodoManagement.Application.Todos.Commands.CreateTodo;
using TodoManagement.Application.Todos.Commands.CompleteTodo;
using TodoManagement.Application.Todos.Commands.CancelTodo;
using TodoManagement.Application.Todos.Queries.GetTodoDetail;
using TodoManagement.Application.Todos.Queries.GetUserTodos;

namespace TodoManagement.Api.Controllers;

[ApiController]
[Route("api/todos")]
public sealed class TodosController : ControllerBase
{
    private readonly ICurrentUser _currentUser;
    private readonly CreateTodoCommandHandler _createTodoHandler;
    private readonly CompleteTodoCommandHandler _completeTodoHandler;
    private readonly CancelTodoCommandHandler _cancelTodoHandler;
    private readonly GetTodoDetailQueryHandler _getTodoDetailHandler;
    private readonly GetUserTodosQueryHandler _getUserTodosHandler;

    public TodosController(
        ICurrentUser currentUser,
        CreateTodoCommandHandler createTodoHandler,
        CompleteTodoCommandHandler completeTodoHandler,
        CancelTodoCommandHandler cancelTodoHandler,
        GetTodoDetailQueryHandler getTodoDetailHandler,
        GetUserTodosQueryHandler getUserTodosHandler)
    {
        _currentUser = currentUser;
        _createTodoHandler = createTodoHandler;
        _completeTodoHandler = completeTodoHandler;
        _cancelTodoHandler = cancelTodoHandler;
        _getTodoDetailHandler = getTodoDetailHandler;
        _getUserTodosHandler = getUserTodosHandler;
    }

    // POST /todos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(
            _currentUser.Id,
            request.Title
        );

        await _createTodoHandler.Handle(command);
        return Ok();
    }

    // GET /todos/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetTodoDetailQuery(id);
        var result = await _getTodoDetailHandler.Handle(query);

        if (result is null)
            return NotFound();

        Console.WriteLine(
        $"[DEBUG] GetTodoDetail result = " +
        System.Text.Json.JsonSerializer.Serialize(result)
    );
		return Ok(result);
    }

    // POST /todos/{id}/complete
    [HttpPost("{id:guid}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        await _completeTodoHandler.Handle(
            new CompleteTodoCommand(id)
        );

        return Ok();
    }

    // POST /todos/{id}/cancel
    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id)
    {
        await _cancelTodoHandler.Handle(
            new CancelTodoCommand(id)
        );

        return Ok();
    }

    // GET /todos
    // "My todos" use-case
    [HttpGet]
    public async Task<IActionResult> GetMyTodos()
    {
        var query = new GetUserTodosQuery(_currentUser.Id);
        var result = await _getUserTodosHandler.Handle(query);
Console.WriteLine(
    $"[DEBUG] GetUserTodos result = " +
    System.Text.Json.JsonSerializer.Serialize(result)
);

        return Ok(result);
    }
}
