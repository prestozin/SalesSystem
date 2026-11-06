namespace SalesSystem.Core.Shared;

using SalesSystem.Core.Constants;


public class ProductPagedRequest : PagedRequest
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }

    public ProductPagedRequest()
    {
        Sort = Constants.DEFAULT_SORT_VALUE;
        Order = Constants.DEFAULT_ORDER_VALUE;
    }
}
