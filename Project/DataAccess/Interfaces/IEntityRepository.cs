using System.Linq.Expressions;

namespace Project.DataAccess.Interfaces;

public interface IEntityRepository<TEntity> where TEntity : class
{
    Task<TEntity> Get(Expression<Func<TEntity, bool>> filter);

    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null);

    Task<(List<TEntity>, int totalCount)> GetPagedResponse(int pageNumber, int pageSize);

    Task<TEntity> Add(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(TEntity entity);
}