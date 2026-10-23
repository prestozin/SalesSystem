using Microsoft.AspNetCore.Mvc;
using SalesSytem.Application.DTOs.Order.Request;
using SalesSytem.Application.Interfaces;

namespace sales_system.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var result = await orderService.CreateAsync(request);

        if (!result.IsSuccess)
            return BadRequest(result);

        return StatusCode(StatusCodes.Status201Created, result);
    }
}