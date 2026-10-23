
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using SalesSytem.Application.Interfaces;
using SalesSytem.Application.Mappings;
using SalesSytem.Application.Services;

namespace SalesSytem.Application.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();

        TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(ProductMapping).Assembly);
        config.Scan(typeof(OrderMapping).Assembly);

        return services;
    }
}
