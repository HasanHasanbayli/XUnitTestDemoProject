using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Contexts;
using Project.DataAccess.Interfaces;
using Project.Entities;

namespace Project.DataAccess;

public class ProductDal : IProductDal
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductDal(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Product> Get(Expression<Func<Product, bool>> filter)
    {
        return (await _applicationDbContext.Set<Product>().SingleOrDefaultAsync(filter))!;
    }

    public async Task<List<Product>> GetAll(Expression<Func<Product, bool>>? filter = null)
    {
        return filter == null
            ? await _applicationDbContext.Set<Product>().ToListAsync()
            : await _applicationDbContext.Set<Product>().Where(filter).ToListAsync();
    }

    public async Task<(List<Product>, int totalCount)> GetPagedResponse(int pageNumber, int pageSize)
    {
        var totalCount = await _applicationDbContext.Products!.CountAsync();

        var products = await _applicationDbContext
            .Set<Product>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();

        return (products, totalCount);
    }

    public async Task<Product> Add(Product product)
    {
        _applicationDbContext.Entry(product).State = EntityState.Added;

        await _applicationDbContext.SaveChangesAsync();

        return product;
    }

    public async Task Update(Product product)
    {
        _applicationDbContext.Entry(product).State = EntityState.Modified;

        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task Delete(Product product)
    {
        _applicationDbContext.Entry(product).State = EntityState.Deleted;

        await _applicationDbContext.SaveChangesAsync();
    }
}