using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.Shared;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.Interfaces;

namespace SalesSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductService _productService;
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{id}/Get")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productService.GetByIdAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        var result = await _productService.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id}/Update")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request)
    {
        var result = await _productService.UpdateAsync(id, request);

        if (!result.IsSuccess)
            return NotFound(result);
     
        return Ok(result);
    }

    [HttpPatch("{id}/ToggleStatus")]
    public async Task<IActionResult> ToggleStatus(int id)
    {
        var result = await _productService.ToggleStatusAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);
        
        return Ok(result);
    }

    [HttpPatch("{id}/Restock")]
    public async Task<IActionResult> Restock(int id, RestockProductRequest request)
    {
        var result = await _productService.RestockAsync(id, request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("GetPaged")]
    public async Task<IActionResult> GetPaged([FromQuery] ProductPagedRequest pagedParams)
    {
        var result = await _productService.GetPagedAsync(pagedParams);

        return Ok(result);
    }
}