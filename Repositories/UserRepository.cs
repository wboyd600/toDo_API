using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using toDo_API.Models;
using toDo_API.db;
using toDo_API.Helpers;
using System.Text;

namespace toDo_API.Repositories
{
    public interface IUserRepository : IRepository<User, Guid> {}

        public class UserRepository : IUserRepository {
        private readonly ApplicationContext _context;
        private DbSet<User> _dbSet => _context.Set<User>();

        public UserRepository (
            ApplicationContext context
        ) {
            _context = context;
        }

        public async Task<IEnumerable<User>> All(Expression<Func<User, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<User?> Get(params object[] values) {
            return await _dbSet.FindAsync(values);
        }

        public async Task<User?> Get(String username) {
            return await _dbSet.Where(i => i.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User> Create(User entity)
        {
            var currentUser = await Get(entity.Username);
            if (currentUser != null) {
                throw new InvalidOperationException("Username already exists");
            }
            var newUser = new User();
            newUser.Username = entity.Username;
            var salt = Helpers.Crypto.GenerateSalt();
            newUser.Salt = salt;
            newUser.Password = Helpers.Crypto.ComputeHash(entity.Password, salt);
            
            // newUser.salt
            var added = _dbSet.Add(newUser);
            await _context.SaveChangesAsync();
            return added.Entity;
        }

        // public async Task<User?> Login(User entity) {
        //     var currentUser = await Get(entity.Username);
        //     if (currentUser == null) {
        //         throw new InvalidOperationException("Username doesn't exist");
        //     }
        //     var salt = currentUser.Salt;
        //     var validPassword = Helpers.Crypto.VerifyPassword(entity.Password, currentUser.Password, salt);
        //     if (validPassword) {
        //         return currentUser;
        //     } else {
        //         return null;
        //     }
        // }

        public Task<User?> Update(Guid index, User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User?> Delete(Guid index)
        {
            throw new NotImplementedException();
        }
    }
}