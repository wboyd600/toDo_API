using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using toDo_API.Models;
using toDo_API.db;

namespace toDo_API.Repositories
{
    public interface ITodoRepository : IRepository<Todo, Guid> {}

    public class TodoRepository : ITodoRepository {
        private readonly ApplicationContext _context;
        private DbSet<Todo> _dbSet => _context.Set<Todo>();

        public TodoRepository (
            ApplicationContext context
        ) {
            _context = context;
        }

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

        public async Task<IEnumerable<Todo>> All(Expression<Func<Todo, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<Todo> Create(Todo entity) 
        {
            var todo = _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return todo.Entity;
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

        public Todo? Update(Guid index, Todo entity)
        {
            var result = Get(index);

            if (result != null) {
                result.Title = entity.Title;
                result.Id = entity.Id;
                result.Due = entity.Due;
                result.Completed = entity.Completed;
            }

            return result;
        }
    }
}