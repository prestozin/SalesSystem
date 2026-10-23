using Microsoft.AspNetCore.Mvc;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.Interfaces;

namespace SalesSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> GetAll()
    {
        var result = await productService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}/get")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await productService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await productService.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var result = await productService.UpdateAsync(id, request);

        if (!result.IsSuccess)
            return NotFound(result);
     
        return Ok(result);
    }

    [HttpPatch("{id}/toggleStatus")]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var result = await productService.ToggleStatusAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);
        
        return Ok(result);
    }

    [HttpPatch("{id}/restock")]
    public async Task<IActionResult> Restock(int id, RestockProductRequest request)
    {
        var result = await productService.RestockAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}