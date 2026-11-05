using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSystem.Core.Shared;
using SalesSystem.Infra.Data;

namespace SalesSystem.Infra.Data.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
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

    public async Task<(IEnumerable<Order> orders, int totalCount)> GetPagedAsync(Guid userId, OrderPagedParamsRequest pagedParams)
    {
        IQueryable<Order> query = _context.Orders
            .AsNoTracking()
            .Where(order => order.UserId == userId)
            .Include(order => order.Status)
            .Include(order => order.Items)
                .ThenInclude(item => item.Product);

        query = ApplyFilters(query, pagedParams);

        string sort = GetSortProperty(pagedParams.Sort);
        string order = pagedParams.Order ?? "desc";

        return await base.GetPagedAsync(query, pagedParams.PageNumber, pagedParams.PageSize, sort, order);
    }

    private static IQueryable<Order> ApplyFilters(IQueryable<Order> query, OrderPagedParamsRequest pagedParams)
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