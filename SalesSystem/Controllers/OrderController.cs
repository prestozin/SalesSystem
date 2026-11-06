using Microsoft.AspNetCore.Mvc;
using SalesSystem.Core.Shared;
using SalesSytem.Application.DTOs.Order.Request;
using SalesSytem.Application.Interfaces;
using SalesSytem.Application.Services;
using System.Security.Claims;

namespace sales_system.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var result = await _orderService.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpGet("GetPaged")]
    public async Task<IActionResult> GetPaged([FromQuery] OrderPagedRequest pagedParams)
    {
        Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _orderService.GetPagedAsync(userId, pagedParams);

        return Ok(result);
    }
}