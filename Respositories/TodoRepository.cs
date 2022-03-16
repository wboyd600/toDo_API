
using toDo_API.Models;

namespace toDo_API.Repositories
{
    public interface ITodoRepository : IRepository<Todo, Guid> {}

    public class TodoRepository : ITodoRepository {
        public static IDictionary<Guid, Todo> todos = new Dictionary<Guid, Todo>(){
            {
                Guid.Parse("236cb2ad-d971-4bf1-88c7-3c70b6003fa7"), new Todo {
                    Id = Guid.Parse("236cb2ad-d971-4bf1-88c7-3c70b6003fa7"),
                    Title = "Make breaky",
                    Due = DateTime.Parse("03-15-2022"),
                    Completed = false
                }
        }
        };

        public IEnumerable<Todo> All() {
            return todos.Values.ToList();
        }

        public Todo Create(Todo entity)
        {
            var newId = Guid.NewGuid();
            entity.Id = newId;
            todos.Add(newId, entity);
            return todos[newId];
        }

        public Todo? Delete(Guid index)
        {
            var result = Get(index);

            if (result != null) {
                todos.Remove(index);
            }
            
            return result;
        }

        public Todo? Get(Guid index) {
            return todos.ContainsKey(index) ? todos[index] : null;
        }

        public Todo Update(Guid index, Todo entity)
        {
            throw new NotImplementedException();
        }
    }
}