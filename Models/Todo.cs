using toDo_API.Repositories;

namespace toDo_API.Models
{
    public class Todo: IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime Created { get; private set; } = DateTime.UtcNow;
        public DateTime Due { get; set; }
        public bool Completed { get; set; }
    }
}
