using SalesSystem.Core.Entities;
using SalesSystem.Core.Shared;

namespace SalesSystem.Core.Interfaces;

public interface IOrderRepository
{
    Task CreateAsync(Order order, List<Product> products);
    Task<(IEnumerable<Order> orders, int totalCount)> GetPagedAsync(Guid userId, OrderPagedParamsRequest pagedParams);
}