using SalesSystem.Core.Entities;
using SalesSystem.Core.Shared;

namespace SalesSystem.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetByIdsAsync(List<int> ids);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task<(IEnumerable<Product> products, int totalCount)> GetPagedAsync(ProductPagedRequest pagedParams);
}
