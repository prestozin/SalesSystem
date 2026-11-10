using SalesSystem.Core.Shared;

namespace SalesSystem.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(IQueryable<T> query, PagedRequest pagedRequest);
}
