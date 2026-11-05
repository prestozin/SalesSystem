using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSystem.Core.Shared;

namespace SalesSystem.Infra.Data.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.
            AsNoTracking().
            ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetByIdsAsync(List<int> ids)
    {
        return await _context.Products
            .Where(product => ids.Contains(product.Id))
            .ToListAsync();
    }

    public async Task<(IEnumerable<Product> products, int totalCount)> GetPagedAsync(Guid userId, ProductPagedParamsRequest pagedParams)
    {
        IQueryable<Product> query = _context.Products
            .AsNoTracking();

        query = ApplyFilters(query, pagedParams);

        string sort = GetSortProperty(pagedParams.Sort);
        string order = pagedParams.Order ?? "desc";

        return await base.GetPagedAsync(query, pagedParams.PageNumber, pagedParams.PageSize, sort, order);
    }

    private static IQueryable<Product> ApplyFilters(IQueryable<Product> query, ProductPagedParamsRequest pagedParams)
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
