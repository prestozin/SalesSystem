using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;

namespace SalesSystem.Infra.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<List<Product>> GetAllAsync()
    {
        return await context.Products.
            AsNoTracking().
            ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }
}
