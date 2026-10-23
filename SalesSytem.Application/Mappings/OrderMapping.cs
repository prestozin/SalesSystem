using Mapster;
using SalesSystem.Core.Entities;
using SalesSytem.Application.DTOs.Order.Request;
using SalesSytem.Application.DTOs.Order.Response;

namespace SalesSytem.Application.Mappings;

public class OrderMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateOrderItemRequest, OrderItem>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.OrderId)
            .Ignore(dest => dest.Order)
            .Ignore(dest => dest.Product)
            .Ignore(dest => dest.UnitPrice);

        config.NewConfig<OrderItem, OrderItemResponse>()
            .Map(dest => dest.ProductName, src => src.Product.Name);

        config.NewConfig<Order, OrderResponse>()
            .Map(dest => dest.Status, src => (SalesSystem.Core.Enums.EOrderStatus)src.StatusId)
            .Map(dest => dest.Items, src => src.Items);
    }
}