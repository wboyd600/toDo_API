using System.Linq.Expressions;

namespace toDo_API.Repositories
{
    public interface IEntity<TIndex>
    {
        TIndex Id { get; set; }
    }

    public interface IRepository<TEntity, TIndex>
        where TEntity: IEntity<TIndex>
    {
        IEnumerable<TEntity> All();
        TEntity? Get(params object[] values);
        TEntity Create(TEntity entity);
        TEntity Update(TIndex index, TEntity entity);
        TEntity Delete(TIndex index);
    }
}