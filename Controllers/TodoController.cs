using toDo_API.Models;
using Microsoft.AspNetCore.Mvc;
using toDo_API.Repositories;
namespace toDo_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _todoRepository;
    public TodoController(
        ITodoRepository todoRepository
    )
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll() {
        var results = await _todoRepository.All(todo => true);
        return results.ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> Get(Guid id) {
        var todo = await _todoRepository.Get(id);

        if (todo is null)
            return NotFound();

        return Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> Create([FromBody]Todo todo)
    {
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
    public async Task<ActionResult<Todo>> Delete(Guid id)
    {
        var result = await _todoRepository.Delete(id);
        
        if (result is null) {
            return NotFound();
        }

        return Ok(result);
    }
}
