using FluentValidation;
using SalesSystem.Core.Constants;
using SalesSytem.Application.DTOs.Order.Request;
using SalesSytem.Application.Validators.Order;

namespace SalesSytem.Application.Validators.Orders;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(request => request.Items)
            .NotEmpty()
                .WithMessage(Messages.ORDER_ITEMS_REQUIRED);

        RuleForEach(request => request.Items)
            .SetValidator(new CreateOrderItemValidator());
    }
}