using Microsoft.AspNetCore.Mvc;
using Project.DTOs;
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

    [HttpGet]
    public IActionResult Get()
    {
        var result = _productService.GetAll();

        if (result == null!) return NotFound();

        return Ok(result);
    }

    [HttpGet("{productId:int}")]
    public IActionResult Get(int productId)
    {
        var product = _productService.Get(productId);

        if (product == null!) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public IActionResult Post(ProductCreateDto productCreateDto)
    {
        var result = _productService.Add(productCreateDto);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpPut]
    public IActionResult Put(ProductUpdateDto productUpdateDto)
    {
        var result = _productService.Update(productUpdateDto);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete(int productId)
    {
        var result = _productService.Delete(productId);

        if (result)
        {
            return Ok();
        }

        return BadRequest();
    }
}