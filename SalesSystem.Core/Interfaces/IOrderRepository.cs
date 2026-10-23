using SalesSystem.Core.Entities;

namespace SalesSystem.Core.Interfaces;

public interface IOrderRepository
{
    Task CreateAsync(Order order, List<Product> products);
}