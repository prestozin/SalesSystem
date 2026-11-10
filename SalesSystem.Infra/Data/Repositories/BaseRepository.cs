using Microsoft.EntityFrameworkCore;
using SalesSystem.Core.Interfaces;
using SalesSystem.Core.Shared;
using System.Linq.Expressions;

namespace SalesSystem.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(IQueryable<T> query, PagedRequest pagedRequest)
    {
        int totalCount = await query.CountAsync();

        query = ApplySorting(query, pagedRequest.Sort ?? "CreatedAt", pagedRequest.Order ?? "desc");

        IEnumerable<T> items = await query
            .Skip((pagedRequest.PageNumber - 1) * pagedRequest.PageSize)
            .Take(pagedRequest.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    private static IQueryable<T> ApplySorting(IQueryable<T> query, string sort, string order)
    {
        var parameter = Expression.Parameter(typeof(T), "entity");
        var property = Expression.Property(parameter, sort);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = order.Equals("asc", StringComparison.OrdinalIgnoreCase) ? "OrderBy" : "OrderByDescending";

        var expression = Expression.Call(typeof(Queryable), methodName, [typeof(T), property.Type], query.Expression, Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(expression);
    }
}