
namespace SalesSystem.Core.Shared;

public class PagedParamsRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Sort { get; set; }
    public string? Order { get; set; }
}
