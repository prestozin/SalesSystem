using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.DTOs.Product.Response;
using TaskManager.Application.DTOs;

namespace SalesSytem.Application.Interfaces;

public interface IProductService
{
    Task<ResultResponse<ProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ResultResponse<List<ProductResponse>>> GetAllAsync();
    Task<ResultResponse<ProductResponse>> GetByIdAsync(int id);
}
