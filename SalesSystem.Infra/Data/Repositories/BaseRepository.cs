
using SalesSystem.Core.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    public async Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(IQueryable<T> query, int pageNumber, int pageSize, string sort, string order)
    {
        int totalCount = await query.CountAsync();

        var parameter = Expression.Parameter(typeof(T), "entity");
        var property = Expression.Property(parameter, sort);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = order.Equals("asc", StringComparison.OrdinalIgnoreCase) ? "OrderBy" : "OrderByDescending";

        var expression = Expression.Call(typeof(Queryable), methodName, [typeof(T), property.Type], query.Expression, Expression.Quote(lambda));

        query = query.Provider.CreateQuery<T>(expression);

        IEnumerable<T> items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}
