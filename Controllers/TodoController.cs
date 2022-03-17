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
    public ActionResult<Todo> Get(Guid id) {
        var todo = _todoRepository.Get(id);

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
    public IActionResult Update(Guid id, Todo todo)
    {
        var result = _todoRepository.Update(id, todo);

        if (result is null)
            return NotFound();

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _todoRepository.Delete(id);
        
        if (result is null) {
            return NotFound();
        }

        return Ok(result);
    }
}
