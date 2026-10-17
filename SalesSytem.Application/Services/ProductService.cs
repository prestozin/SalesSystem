using Mapster;
using FluentValidation;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.DTOs.Product.Response;
using SalesSytem.Application.Interfaces;
using TaskManager.Application.DTOs;
using SalesSystem.Application.Validators.Products;

namespace SalesSytem.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository) 
    {
        _productRepository = productRepository;
    }

    public async Task<ResultResponse<ProductResponse>> CreateAsync(CreateProductRequest request)
    {
        request.Name = request.Name.Trim();
        request.Description = request.Description.Trim();

        CreateProductValidator validator = new CreateProductValidator();
        await validator.ValidateAndThrowAsync(request);

        Product product = request.Adapt<Product>();

        await _productRepository.AddAsync(product);

        ProductResponse response = product.Adapt<ProductResponse>();

        return ResultResponse<ProductResponse>.Success(response);
    }

    public async Task<ResultResponse<List<ProductResponse>>> GetAllAsync()
    {
        List<Product> products = await _productRepository.GetAllAsync();

        List<ProductResponse> responses = products.Adapt<List<ProductResponse>>();

        return ResultResponse<List<ProductResponse>>.Success(responses);
    }

    public async Task<ResultResponse<ProductResponse>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return ResultResponse<ProductResponse>.Failure("ID do produto inválido.");

        Product? product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ResultResponse<ProductResponse>.Failure("Produto não encontrado.");

        ProductResponse response = product.Adapt<ProductResponse>();

        return ResultResponse<ProductResponse>.Success(response);
    }
}
