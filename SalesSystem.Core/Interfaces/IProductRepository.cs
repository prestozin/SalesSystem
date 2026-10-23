using SalesSystem.Core.Entities;

namespace SalesSystem.Core.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetByIdsAsync(List<int> ids);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
}
