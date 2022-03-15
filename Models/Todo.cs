namespace toDo_API.Models;

public class Todo
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime Created { get; set; }
    public DateTime Due { get; set; }
    public bool Completed { get; set; }
}