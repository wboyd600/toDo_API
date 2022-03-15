using toDo_API.Models;
using toDo_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace toDo_API.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    public TodoController()
    {
    }

    [HttpGet]
    public ActionResult<List<Todo>> GetAll() =>
        TodoService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Todo> Get(Guid id) {
        var todo = TodoService.GetTodo(id);

        if (todo is null)
            return NotFound();

        return todo;
    }

    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        TodoService.Add(todo);
        return CreatedAtAction(nameof(Create), new { id = todo.Id }, todo);
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
