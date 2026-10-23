using Mapster;
using SalesSystem.Core.Entities;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.DTOs.Product.Response;

namespace SalesSytem.Application.Mappings;

public class ProductMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductRequest, Product>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.ReservedQuantity)
            .Ignore(dest => dest.IsActive)
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.UpdatedAt);

        config.NewConfig<UpdateProductRequest, Product>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.ReservedQuantity)
            .Ignore(dest => dest.IsActive)
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.UpdatedAt);

        config.NewConfig<Product, ProductResponse>();
    }
}
