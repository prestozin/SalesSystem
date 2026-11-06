namespace SalesSystem.Core.Shared;

using SalesSystem.Core.Constants;
public class OrderPagedRequest : PagedRequest
{
    public string? Status { get; set; }

    public OrderPagedRequest()
    {
        Sort = Constants.DEFAULT_SORT_VALUE;
        Order = Constants.DEFAULT_ORDER_VALUE;
    }
}
