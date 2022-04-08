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
    public async Task<ActionResult<IEnumerable<Todo>>> GetAll(bool? completed = null, string? field = null, string? order = null)
    {
        var userID = new Guid(_userService.GetMyID());
        System.Linq.Expressions.Expression<Func<Todo, bool>> expression = (todo => todo.userid == userID);
        
        if (completed != null) {
            expression = expression => expression.Completed == completed;
        }

        var results = await _todoRepository.All(expression);
        var validField = new List<String>(){
            "created",
            "due",
            "title"
        };

        var validOrder = new List<String>(){
            "asc",
            "desc"
        };
        if (field != null && order != null)
        {
            var ascending = (order == "asc");

            if (validField.Contains(field) && validOrder.Contains(order))
            {

                if (field == "created") {
                    if (ascending) {
                        results = results.OrderBy(p => p.Created);
                    } else {
                        results = results.OrderByDescending(p => p.Created);
                    }
                } else if (field == "due") {
                    if (ascending) {
                        results = results.OrderBy(p => p.Due);
                    } else {
                        results = results.OrderByDescending(p => p.Due);
                    }
                } else if (field == "title") {
                    if (ascending) {
                        results = results.OrderBy(p => p.Title);
                    } else {
                        results = results.OrderByDescending(p => p.Title);
                    }
                }
            }
        }
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
    [Authorize]
    public async Task<ActionResult<Todo>> Update(Guid id, [FromBody] Todo todo)
    {
        var updateTodo = await _todoRepository.Get(id);
        var userID = new Guid(_userService.GetMyID());

        if (updateTodo is null || userID != todo.userid)
            return NotFound();

        updateTodo.Completed = todo.Completed;
        var updatedTodo = await _todoRepository.Update(id, updateTodo);

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
