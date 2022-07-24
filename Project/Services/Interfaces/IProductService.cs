using Project.DTOs;
using Project.Entities;

namespace Project.Services.Interfaces;

public interface IProductService
{
    Product Get(int productId);
    List<Product> GetAll();
    bool Add(ProductCreateDto productCreateDto);
    bool Update(ProductUpdateDto productUpdateDto);
    bool Delete(int productId);
}