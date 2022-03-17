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

        public async Task<Todo?> Delete(Guid index)
        {
            var result = _dbSet.Remove(new Todo { Id = index });

            await _context.SaveChangesAsync();

            return result.Entity;
        }

         public async Task<Todo?> Get(params object[] values) {
            return await _dbSet.FindAsync(values);
        }

        public async Task<Todo?> Update(Guid index, Todo entity)
        {
            entity.Id = index;
            var todo = _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return todo.Entity;
        }
    }
}