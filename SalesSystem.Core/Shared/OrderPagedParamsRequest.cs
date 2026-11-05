namespace SalesSystem.Core.Shared;

using SalesSystem.Core.Constants;
public class OrderPagedParamsRequest : PagedParamsRequest
{
    public string? Status { get; set; }

    public OrderPagedParamsRequest()
    {
        Sort = Constants.DEFAULT_SORT_VALUE;
        Order = Constants.DEFAULT_ORDER_VALUE;
    }
}
