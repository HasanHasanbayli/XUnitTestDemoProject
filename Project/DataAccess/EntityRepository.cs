using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Contexts;
using Project.DataAccess.Interfaces;

namespace Project.DataAccess;

public class EntityRepository<T> : IEntityRepository<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext;

    public EntityRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter)
    {
        return (await _applicationDbContext.Set<T>().SingleOrDefaultAsync(filter))!;
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null)
    {
        return filter == null
            ? await _applicationDbContext.Set<T>().ToListAsync()
            : await _applicationDbContext.Set<T>().Where(filter).ToListAsync();
    }

    public async Task<(List<T>, int totalCount)> GetPagedResponse(int pageNumber, int pageSize)
    {
        var totalCount = await _applicationDbContext.Set<T>().CountAsync();

        var products = await _applicationDbContext
            .Set<T>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return (products, totalCount);
    }

    public async Task<T> Add(T entity)
    {
        _applicationDbContext.Entry(entity).State = EntityState.Added;

        await _applicationDbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Update(T entity)
    {
        _applicationDbContext.Entry(entity).State = EntityState.Modified;

        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _applicationDbContext.Entry(entity).State = EntityState.Deleted;

        await _applicationDbContext.SaveChangesAsync();
    }
}