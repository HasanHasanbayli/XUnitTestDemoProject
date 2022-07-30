using System.Linq.Expressions;
using Project.DTOs;
using Project.Entities;
using Project.Results;
using Project.Wrappers;
using IResult = Project.Results.IResult;

namespace Project.Services.Interfaces;

public interface IProductService
{
    Task<IDataResult<Product>> Get(int productId);

    Task<IDataResult<Product>> Add(ProductCreateDto productCreateDto);

    Task<IResult> Update(ProductUpdateDto productUpdateDto);

    Task<IDataResult<PagedResponse<List<Product>>>> GetPagedResponse(int pageNumber, int pageSize);

    Task<IResult> Delete(int productId);
}