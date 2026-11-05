namespace SalesSystem.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(IQueryable<T> query, int pageNumber, int pageSize, string sort, string order);
}
