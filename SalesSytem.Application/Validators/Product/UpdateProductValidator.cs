using FluentValidation;
using SalesSystem.Core.Constants;
using SalesSytem.Application.DTOs.Product.Request;

namespace SalesSystem.Application.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
                .WithMessage(Messages.PRODUCT_NAME_REQUIRED)
            .MaximumLength(100)
                .WithMessage(Messages.PRODUCT_NAME_MAX_LENGTH);

        RuleFor(product => product.Description)
            .NotEmpty()
                .WithMessage(Messages.PRODUCT_DESCRIPTION_REQUIRED)
            .MaximumLength(500)
                .WithMessage(Messages.PRODUCT_DESCRIPTION_MAX_LENGTH);

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage(Messages.PRODUCT_PRICE_INVALID);

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
                .WithMessage(Messages.PRODUCT_STOCK_QUANTITY_INVALID);

        RuleFor(product => product.ImageUrl)
            .MaximumLength(2048)
                .WithMessage(Messages.PRODUCT_IMAGE_URL_MAX_LENGTH)
            .When(product => product.ImageUrl != null);
    }
}