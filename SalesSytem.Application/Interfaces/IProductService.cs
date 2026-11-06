using SalesSystem.Core.Shared;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.DTOs.Product.Response;
using TaskManager.Application.DTOs;

namespace SalesSytem.Application.Interfaces;

public interface IProductService
{
    Task<ResultResponse<ProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ResultResponse<ProductResponse>> GetByIdAsync(int id);
    Task<ResultResponse<ProductResponse>> UpdateAsync(int id, UpdateProductRequest request);
    Task<ResultResponse<ProductResponse>> ToggleStatusAsync(int id);
    Task<ResultResponse<ProductResponse>> RestockAsync(int id, RestockProductRequest request);
    Task<ResultResponse<PagedResponse<ProductResponse>>> GetPagedAsync(ProductPagedRequest pagedParams);
}
