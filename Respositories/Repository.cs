using System.Linq.Expressions;
using toDo_API.Models;

namespace toDo_API.Repositories
{
    public interface IEntity<TIndex>
    {
        TIndex Id { get; set; }
    }

    public interface IRepository<TEntity, TIndex>
        where TEntity: IEntity<TIndex>
    {
        public static IDictionary<Guid, Todo>? todos { get; set; }
        Task<IEnumerable<TEntity>> All(
            Expression<Func<TEntity, bool>> predicate
        );
        TEntity? Get(TIndex index);
        Task<TEntity> Create(TEntity entity);
        TEntity? Update(TIndex index, TEntity entity);
        TEntity? Delete(TIndex index);
    }
}
