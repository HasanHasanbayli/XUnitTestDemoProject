using System.Linq.Expressions;
using Project.Entities;

namespace Project.DataAccess.Interfaces;

public interface IProductDal
{
    Task<Product> Get(Expression<Func<Product, bool>> filter);

    Task<List<Product>> GetAll(Expression<Func<Product, bool>>? filter = null);

    Task<(List<Product>, int totalCount)> GetPagedResponse(int pageNumber, int pageSize);

    Task<Product> Add(Product product);

    Task Update(Product product);

    Task Delete(Product product);
}