using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSystem.Core.Shared;

namespace SalesSystem.Infra.Data.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task CreateAsync(Order order, List<Product> products)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        _context.Orders.Add(order);

        foreach (Product product in products)
        {
            _context.Products.Update(product);
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task<(IEnumerable<Order> orders, int totalCount)> GetPagedAsync(Guid userId, OrderPagedRequest pagedParams)
    {
        IQueryable<Order> query = _context.Orders
            .AsNoTracking()
            .Where(order => order.UserId == userId)
            .Include(order => order.Status)
            .Include(order => order.Items)
                .ThenInclude(item => item.Product);

        query = ApplyFilters(query, pagedParams);

        pagedParams.Sort = GetSortProperty(pagedParams.Sort);

        return await base.GetPagedAsync(query, pagedParams);
    }

    private static IQueryable<Order> ApplyFilters(IQueryable<Order> query, OrderPagedRequest pagedParams)
    {
        if (!string.IsNullOrWhiteSpace(pagedParams.Status))
            query = query.Where(order => order.Status.Name == pagedParams.Status);

        return query;
    }

    private static string GetSortProperty(string? sort)
    {
        return sort switch
        {
            nameof(Order.Total) => nameof(Order.Total),
            nameof(Order.StatusId) => nameof(Order.StatusId),
            _ => nameof(Order.CreatedAt)
        };
    }
}