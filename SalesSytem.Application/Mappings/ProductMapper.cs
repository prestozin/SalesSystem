using Mapster;
using SalesSystem.Core.Entities;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.DTOs.Product.Response;

namespace SalesSytem.Application.Mappings;

public class ProductMapping : IRegister
{
  public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<CreateProductRequest, Product>
            .NewConfig()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.ReservedQuantity)
            .Ignore(dest => dest.IsActive)
            .Ignore(dest => dest.CreatedAt)
            .Ignore(dest => dest.UpdatedAt);

        TypeAdapterConfig<Product, ProductResponse>.NewConfig();
    }
}
