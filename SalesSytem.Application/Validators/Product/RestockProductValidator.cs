using FluentValidation;
using SalesSystem.Core.Constants;
using SalesSytem.Application.DTOs.Product.Request;

namespace SalesSytem.Application.Validators.Products;

public class RestockProductValidator : AbstractValidator<RestockProductRequest>
{
    public RestockProductValidator()
    {
        RuleFor(request => request.Quantity)
            .GreaterThan(0)
                .WithMessage(Messages.PRODUCT_RESTOCK_QUANTITY_INVALID);
    }
}