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
        Task<IEnumerable<TEntity>> All(
            Expression<Func<TEntity, bool>> predicate
        );
        Task<TEntity?> Get(params object[] values);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity?> Update(TIndex index, TEntity entity);
        Task<TEntity?> Delete(TIndex index);
    }
}
