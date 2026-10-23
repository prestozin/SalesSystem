namespace SalesSytem.Application.DTOs.Order.Request;

public class CreateOrderItemRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}