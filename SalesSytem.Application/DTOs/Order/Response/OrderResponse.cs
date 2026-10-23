using SalesSystem.Core.Enums;

namespace SalesSytem.Application.DTOs.Order.Response;

public class OrderResponse
{
    public int Id { get; set; }
    public EOrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderItemResponse> Items { get; set; } = [];
}