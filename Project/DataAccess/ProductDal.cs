using Project.DataAccess.Contexts;
using Project.DataAccess.Interfaces;
using Project.Entities;

namespace Project.DataAccess;

public class ProductDal : EntityRepository<Product>, IProductDal
{
    public ProductDal(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}