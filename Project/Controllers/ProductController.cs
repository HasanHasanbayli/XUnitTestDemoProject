using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
using Project.Filter;
using Project.Services.Interfaces;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId:int}")]
    public async Task<IActionResult> Get(int productId)
    {
        var result = await _productService.Get(productId);

        if (!result.Success) return NotFound(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productService.GetAll();

        if (!result.Success) return NotFound(result);

        return Ok(result);
    }

    [HttpGet("getPagedResponse")]
    public async Task<IActionResult> GetPagedResponse([FromQuery] PaginationFilter filter)
    {
        var result = await _productService.GetPagedResponse(filter.PageNumber, filter.PageSize);

        if (!result.Success) return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductCreateDto productCreateDto)
    {
        // var fixture = new Fixture();

        // for (var i = 0; i < 2000; i++)
        // {
        // var productCreateDto = fixture.Create<ProductCreateDto>();
        // }
        var result = await _productService.Add(productCreateDto);

        if (!result.Success) return BadRequest(result);
        
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put(ProductUpdateDto productUpdateDto)
    {
        var result = await _productService.Update(productUpdateDto);

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int productId)
    {
        var result = await _productService.Delete(productId);

        if (!result.Success) return BadRequest(result);

        return Ok(result);
    }
}