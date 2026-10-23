using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSystem.Infra.Data;

namespace SalesSystem.Infra.Data.Repositories;

public class OrderRepository : IOrderRepository
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
}