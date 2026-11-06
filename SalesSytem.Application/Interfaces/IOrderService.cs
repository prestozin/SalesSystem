using SalesSystem.Core.Shared;
using SalesSytem.Application.DTOs.Order.Request;
using SalesSytem.Application.DTOs.Order.Response;
using TaskManager.Application.DTOs;

namespace SalesSytem.Application.Interfaces;

public interface IOrderService
{
    Task<ResultResponse<OrderResponse>> CreateAsync(CreateOrderRequest request);
    Task<ResultResponse<PagedResponse<OrderResponse>>> GetPagedAsync(Guid userId, OrderPagedRequest pagedParams);
}