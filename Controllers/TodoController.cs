using toDo_API.Models;
using Microsoft.AspNetCore.Mvc;
using toDo_API.Repositories;
using Microsoft.AspNetCore.Authorization;
namespace toDo_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;
    private readonly IUserService _userService;
    public TodoController(
        ITodoRepository todoRepository,
        IUserService userService
    )
    {
        _todoRepository = todoRepository;
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll() {
        var userID = new Guid(_userService.GetMyID());
        var results = await _todoRepository.All(todo => todo.userid == userID);
        return results.ToList();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<Todo>> Get(Guid id) {
        var todo = await _todoRepository.Get(id);
        var userID = new Guid(_userService.GetMyID());

        if (todo is null || userID != todo.userid)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Todo>> Create([FromBody]NewTodoRequestBody newTodo)
    {
        var todo = new Todo();
        todo.userid = new Guid(_userService.GetMyID());
        todo.Completed = newTodo.Completed;
        todo.Title = newTodo.Title;
        todo.Due = newTodo.Due;
        var createdTodo = await _todoRepository.Create(todo);
        return createdTodo;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Todo>> Update(Guid id, [FromBody] Todo todo)
    {
        var updatedTodo = await _todoRepository.Update(id, todo);

        if (updatedTodo is null) {
            return NotFound();
        }

        return updatedTodo;
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Todo>> Delete(Guid id)
    {
        var todo = await _todoRepository.Get(id);
        var userID = new Guid(_userService.GetMyID());
        if (todo is null || userID != todo.userid)
            return NotFound();

        var result = await _todoRepository.Delete(id);
        
        if (result is null) {
            return NotFound();
        }

        return Ok(result);
    }
}
