using System.Linq.Expressions;
using Project.DataAccess.Interfaces;
using Project.DTOs;
using Project.Entities;
using Project.Filter;
using Project.Helpers;
using Project.Results;
using Project.Services.Interfaces;
using Project.Wrappers;
using IResult = Project.Results.IResult;

namespace Project.Services;

public class ProductService : IProductService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductDal _productDal;
    private readonly IUriService _uriService;

    public ProductService(IProductDal productDal, IUriService uriService, IHttpContextAccessor httpContextAccessor)
    {
        _productDal = productDal;
        _uriService = uriService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IDataResult<Product>> Get(int productId)
    {
        var result = await _productDal.Get(x => x.Id == productId);

        if (result == null!) return new ErrorDataResult<Product>(result!, 404, "Product not found");

        return new SuccessDataResult<Product>(result, 200);
    }

    public async Task<IDataResult<PagedResponse<List<Product>>>> GetPagedResponse(int pageNumber, int pageSize)
    {
        var route = _httpContextAccessor.HttpContext!.Request.Path.Value;

        var validFilter = new PaginationFilter(pageNumber, pageSize);

        var products =
            await _productDal.GetPagedResponse(validFilter.PageNumber, validFilter.PageSize);

        var pagedResponse =
            PaginationHelper.CreatePagedResponse(products.Item1, validFilter, products.totalCount, _uriService, route!);

        if (pagedResponse.Data.Count == 0)
            return new ErrorDataResult<PagedResponse<List<Product>>>(pagedResponse, 404, "Data Not Found");

        return new SuccessDataResult<PagedResponse<List<Product>>>(pagedResponse, 200);
    }

    public async Task<IDataResult<Product>> Add(ProductCreateDto productCreateDto)
    {
        Product newProduct = new()
        {
            Name = productCreateDto.Name,
            Quantity = productCreateDto.Quantity,
            Price = productCreateDto.Price
        };

        var product = await _productDal.Add(newProduct);

        if (product == null!) return new ErrorDataResult<Product>(product!, 400, "Product add failed");

        return new SuccessDataResult<Product>(product, 201);
    }

    public async Task<IResult> Update(ProductUpdateDto productUpdateDto)
    {
        var dbProduct = await _productDal.Get(x => x.Id == productUpdateDto.Id);

        if (dbProduct == null!) return new ErrorResult(404, "Product not found");

        dbProduct.Name = productUpdateDto.Name;
        dbProduct.Quantity = productUpdateDto.Quantity;
        dbProduct.Price = productUpdateDto.Price;

        await _productDal.Update(dbProduct);

        return new SuccessResult(201, "Product successfully added");
    }
    
    public async Task<IResult> Delete(int productId)
    {
        var dbProduct = await _productDal.Get(x => x.Id == productId);

        if (dbProduct == null!) return new ErrorResult(404, "Product not found");

        await _productDal.Delete(dbProduct);

        return new SuccessResult(200, "Product successfully deleted");
    }
}