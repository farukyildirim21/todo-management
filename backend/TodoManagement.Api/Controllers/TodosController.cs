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

    // [HttpPost("{id:guid}/complete")]
    // public async Task<IActionResult> Complete(Guid id)
    // {
    //     var command = new CompleteTodoCommand(id);
    //     await _completeTodoHandler.Handle(command);
    //     return Ok();
    // }


    // [HttpPost("{id:guid}/cancel")]
    // public async Task<IActionResult> Cancel(Guid id)
    // {
    //     var command = new CancelTodoCommand(id);
    //     await _cancelTodoHandler.Handle(command);
    //     return Ok();
    // }
    
    // [HttpGet("/users/{userId:guid}/todos")]
    // public async Task<IActionResult> GetUserTodos(Guid userId)
    // {
    //     var query = new GetUserTodosQuery(userId);
    //     var result = await _getUserTodosHandler.Handle(query);
    //     return Ok(result);
    // }



}
