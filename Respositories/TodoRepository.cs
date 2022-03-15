
using toDo_API.Models;

namespace toDo_API.Repositories
{
    public interface ITodoRepository : IRepository<Todo, Guid> {}

    public class TodoRepository : ITodoRepository {
        private IDictionary<Guid, Todo> todos = new Dictionary<Guid, Todo>();

        public IEnumerable<Todo> All() {
            return todos.Values.ToList();
        }

        public Todo Create(Todo entity)
        {
            throw new NotImplementedException();
        }

        public Todo Delete(Guid index)
        {
            throw new NotImplementedException();
        }

        public Todo? Get(params object[] values) {
            throw new NotImplementedException();
        }

        public Todo Update(Guid index, Todo entity)
        {
            throw new NotImplementedException();
        }
    }
}