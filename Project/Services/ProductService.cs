using Project.Contexts;
using Project.DTOs;
using Project.Entities;
using Project.Services.Interfaces;

namespace Project.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Product Get(int productId)
    {
        var result = _applicationDbContext.Products!
            .FirstOrDefault(x => x.Id == productId);

        return result ?? null!;
    }

    public List<Product> GetAll()
    {
        var result = _applicationDbContext.Products!.ToList();

        return !result.Any() ? null! : result;
    }

    public bool Add(ProductCreateDto productCreateDto)
    {
        Product newProduct = new()
        {
            Name = productCreateDto.Name,
            Quantity = productCreateDto.Quantity,
            Price = productCreateDto.Price
        };

        _applicationDbContext.Products!.Add(newProduct);

        _applicationDbContext.SaveChanges();

        return true;
    }

    public bool Update(ProductUpdateDto productUpdateDto)
    {
        var dbProduct = _applicationDbContext.Products!
            .FirstOrDefault(x => x.Id == productUpdateDto.Id);

        if (dbProduct == null) return false;

        dbProduct.Name = productUpdateDto.Name;
        dbProduct.Quantity = productUpdateDto.Quantity;
        dbProduct.Price = productUpdateDto.Price;

        _applicationDbContext.Products!.Update(dbProduct);

        _applicationDbContext.SaveChanges();

        return false;
    }

    public bool Delete(int productId)
    {
        var dbProduct = _applicationDbContext.Products!
            .FirstOrDefault(x => x.Id == productId);

        if (dbProduct == null) return false;

        _applicationDbContext.Products!.Remove(dbProduct);

        _applicationDbContext.SaveChanges();

        return true;
    }
}