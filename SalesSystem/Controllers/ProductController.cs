using Microsoft.AspNetCore.Mvc;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.Interfaces;

namespace SalesSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await productService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await productService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await productService.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}