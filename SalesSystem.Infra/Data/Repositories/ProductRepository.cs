using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSystem.Core.Shared;

namespace SalesSystem.Infra.Data.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Products
            .Where(product => ids.Contains(product.Id))
            .ToListAsync();
    }

    public async Task<(IEnumerable<Product> products, int totalCount)> GetPagedAsync(ProductPagedRequest pagedParams)
    {
        IQueryable<Product> query = _context.Products
            .AsNoTracking();

        query = ApplyFilters(query, pagedParams);

        pagedParams.Sort = GetSortProperty(pagedParams.Sort);

        return await base.GetPagedAsync(query, pagedParams);
    }

    private static IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductPagedRequest pagedParams)
    {
        if (!string.IsNullOrWhiteSpace(pagedParams.Search))
            query = query.Where(product => product.Name.Contains(pagedParams.Search));

        if (pagedParams.IsActive.HasValue)
            query = query.Where(product => product.IsActive == pagedParams.IsActive.Value);

        return query;
    }

    private static string GetSortProperty(string? sort)
    {
        return sort switch
        {
            nameof(Product.Name) => nameof(Product.Name),
            nameof(Product.Price) => nameof(Product.Price),
            nameof(Product.StockQuantity) => nameof(Product.StockQuantity),
            _ => nameof(Product.CreatedAt)
        };
    }
}