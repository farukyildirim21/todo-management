using Microsoft.AspNetCore.Mvc;
using TodoManagement.Api.Contracts.Requests;
using TodoManagement.Application.Todos.Commands.CreateTodo;
using TodoManagement.Application.Todos.Queries.GetTodoDetail;

namespace TodoManagement.Api.Controllers;

[ApiController]
[Route("todos")]
public sealed class TodosController : ControllerBase
{
    private readonly CreateTodoCommandHandler _createTodoHandler;
    private readonly GetTodoDetailQueryHandler _getTodoDetailHandler;

    public TodosController(
        CreateTodoCommandHandler createTodoHandler,
        GetTodoDetailQueryHandler getTodoDetailHandler)
    {
        _createTodoHandler = createTodoHandler;
        _getTodoDetailHandler = getTodoDetailHandler;
    }

    // ---------------------------
    // CREATE TODO
    // POST /todos
    // ---------------------------
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request)
    {
        var command = new CreateTodoCommand(
            request.UserId,
            request.Title
        );

        await _createTodoHandler.Handle(command);

        return Ok();
        // istersen ileride:
        // return CreatedAtAction(nameof(GetById), new { id = todoId }, null);
    }

    // ---------------------------
    // GET TODO DETAIL
    // GET /todos/{id}
    // ---------------------------
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetTodoDetailQuery(id);

        var result = await _getTodoDetailHandler.Handle(query);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
