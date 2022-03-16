using toDo_API.Models;
using toDo_API.Services;
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
    public ActionResult<List<Todo>> GetAll() {
        var results = _todoRepository.All();
        return results.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Todo> Get(Guid id) {
        var todo = TodoService.GetTodo(id);

        if (todo is null)
            return NotFound();

        return todo;
    }

    [HttpPost]
    public IActionResult Create([FromBody]Todo todo)
    {
        var result = _todoRepository.Create(todo);
        return CreatedAtAction(nameof(Create), result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Todo todo)
    {
        if (id != todo.Id)
            return BadRequest();

        var existingTodo = TodoService.GetTodo(id);
        if (existingTodo is null)
            return NotFound();
        
        TodoService.Update(todo);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var todo = TodoService.GetTodo(id);

        if (todo is null)
            return NotFound();

        TodoService.Delete(id);

        return NoContent();
    }
}
