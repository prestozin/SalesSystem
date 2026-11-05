namespace SalesSystem.Core.Shared;

using SalesSystem.Core.Constants;


public class ProductPagedParamsRequest : PagedParamsRequest
{
    public string? Search { get; set; }
    public bool? IsActive { get; set; }

    public ProductPagedParamsRequest()
    {
        Sort = Constants.DEFAULT_SORT_VALUE;
        Order = Constants.DEFAULT_ORDER_VALUE;
    }
}
