using toDo_API.Models;

namespace toDo_API.Services;

public static class TodoService
{
    static List<Todo> Todos { get; }
    static byte nextId = 2;
    static TodoService()
    {
        Todos = new List<Todo>
        {
            new Todo { Id = new Guid("00000000-0000-0000-0000-000000000000"), Title = "Make breakfast", Created = new DateTime(2015, 12, 25), Due = new DateTime(2015, 12, 27), Completed = false},
            new Todo { Id = new Guid("00000000-0000-0000-0000-000000000001"), Title = "Make breakfast", Created = new DateTime(2015, 12, 25), Due = new DateTime(2015, 12, 27), Completed = false},
        };
    }

    public static List<Todo> GetAll() => Todos;

    public static Todo? GetTodo(Guid id) => Todos.FirstOrDefault(t => t.Id == id);

    public static void Add(Todo todo)
    {
        var nextString = "00000000-0000-0000-0000-00000000000";
        nextString += $"{nextId++}";
        todo.Id = new Guid(nextString);
        Todos.Add(todo);
    }

    public static void Delete(Guid id)
    {
        var todo = GetTodo(id);
        if (todo is null) 
            return;

        Todos.Remove(todo);
    }
    
    public static void Update(Todo todo)
    {
        var index = Todos.FindIndex(t => t.Id == todo.Id);
        if (index == -1)
            return;

        Todos[index] = todo;
    }
}