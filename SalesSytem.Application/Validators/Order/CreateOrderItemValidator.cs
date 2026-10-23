using FluentValidation;
using SalesSystem.Core.Constants;
using SalesSytem.Application.DTOs.Order.Request;

namespace SalesSytem.Application.Validators.Order;

public class CreateOrderItemValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemValidator()
    {
        RuleFor(item => item.ProductId)
            .GreaterThan(0)
                .WithMessage(Messages.PRODUCT_INVALID_ID);

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
                .WithMessage(Messages.ORDER_ITEM_QUANTITY_INVALID);
    }
}