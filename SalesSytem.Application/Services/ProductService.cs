using FluentValidation;
using Mapster;
using SalesSystem.Application.Validators;
using SalesSystem.Application.Validators.Products;
using SalesSystem.Core.Constants;
using SalesSystem.Core.Entities;
using SalesSystem.Core.Interfaces;
using SalesSytem.Application.DTOs.Product.Request;
using SalesSytem.Application.DTOs.Product.Response;
using SalesSytem.Application.Interfaces;
using TaskManager.Application.DTOs;

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
            return ResultResponse<ProductResponse>.Failure(Messages.INVALID_PRODUCT_ID);

        Product? product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ResultResponse<ProductResponse>.Failure(Messages.PRODUCT_NOT_FOUND);

        ProductResponse response = product.Adapt<ProductResponse>();

        return ResultResponse<ProductResponse>.Success(response);
    }

    public async Task<ResultResponse<ProductResponse>> UpdateAsync(int id, UpdateProductRequest request)
    {
        if (id <= 0)
            return ResultResponse<ProductResponse>.Failure(Messages.INVALID_PRODUCT_ID);

        Product? product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ResultResponse<ProductResponse>.Failure(Messages.PRODUCT_NOT_FOUND);

        request.Name = request.Name.Trim();
        request.Description = request.Description.Trim();

        UpdateProductValidator validator = new UpdateProductValidator();
        await validator.ValidateAndThrowAsync(request);

        if (request.StockQuantity < product.ReservedQuantity)
            return ResultResponse<ProductResponse>.Failure(Messages.PRODUCT_STOCK_LESS_THAN_RESERVED);

        request.Adapt(product);

        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);

        ProductResponse response = product.Adapt<ProductResponse>();

        return ResultResponse<ProductResponse>.Success(response);
    }

    public async Task<ResultResponse<ProductResponse>> DeactivateAsync(int id)
    {
        if (id <= 0)
            return ResultResponse<ProductResponse>.Failure(Messages.INVALID_PRODUCT_ID);

        Product? product = await _productRepository.GetByIdAsync(id);

        if (product == null)
            return ResultResponse<ProductResponse>.Failure(Messages.PRODUCT_NOT_FOUND);

        if (!product.IsActive)
            return ResultResponse<ProductResponse>.Failure(Messages.PRODUCT_ALREADY_DEACTIVATED);

        product.IsActive = false;
        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product);

        ProductResponse response = product.Adapt<ProductResponse>();

        return ResultResponse<ProductResponse>.Success(response);
    }
}