namespace SalesSytem.Application.DTOs.Order.Request;

public class CreateOrderRequest
{
    public List<CreateOrderItemRequest> Items { get; set; } = [];
}