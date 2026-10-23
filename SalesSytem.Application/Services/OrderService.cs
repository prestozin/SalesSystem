using FluentValidation;
using Mapster;
using SalesSystem.Core.Constants;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Enums;
using SalesSystem.Core.Interfaces;
using SalesSytem.Application.DTOs.Order.Request;
using SalesSytem.Application.DTOs.Order.Response;
using SalesSytem.Application.Interfaces;
using SalesSytem.Application.Validators.Orders;
using TaskManager.Application.DTOs;

namespace SalesSytem.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<ResultResponse<OrderResponse>> CreateAsync(CreateOrderRequest request)
    {
        CreateOrderValidator validator = new();
        await validator.ValidateAndThrowAsync(request);

        List<int> productIds = request.Items
            .Select(item => item.ProductId)
            .ToList();

        if (productIds.Distinct().Count() != productIds.Count)
            return ResultResponse<OrderResponse>.Failure(Messages.ORDER_DUPLICATE_PRODUCTS);

        List<Product> products = await _productRepository.GetByIdsAsync(productIds);

        if (products.Count != productIds.Count || products.Any(product => !product.IsActive))
            return ResultResponse<OrderResponse>.Failure(
                products.Count != productIds.Count ? Messages.ORDER_PRODUCT_NOT_FOUND : Messages.ORDER_PRODUCT_INACTIVE);

        if (HasInsufficientStock(request.Items, products))
            return ResultResponse<OrderResponse>.Failure(Messages.ORDER_INSUFFICIENT_STOCK);

        Order order = CreateOrder(request.Items, products);

        await _orderRepository.CreateAsync(order, products);

        OrderResponse response = order.Adapt<OrderResponse>();

        return ResultResponse<OrderResponse>.Success(response);
    }

    private static bool HasInsufficientStock(List<CreateOrderItemRequest> items, List<Product> products)
    {
        return items.Any(item =>
        {
            Product product = products.Single(product => product.Id == item.ProductId);

            return product.AvailableQuantity < item.Quantity;
        });
    }

    private static Order CreateOrder(List<CreateOrderItemRequest> items, List<Product> products)
    {
        Order order = new()
        {
            StatusId = (int)EOrderStatus.Pending
        };

        items.ForEach(item =>
        {
            Product product = products.Single(product => product.Id == item.ProductId);

            OrderItem orderItem = item.Adapt<OrderItem>();
            orderItem.UnitPrice = product.Price;
            orderItem.Product = product;

            order.Items.Add(orderItem);
            product.ReservedQuantity += item.Quantity;
        });

        order.Total = order.Items.Sum(item => item.Quantity * item.UnitPrice);

        return order;
    }
}