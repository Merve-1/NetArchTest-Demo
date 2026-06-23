using ArcTestsServices.DTOs;

namespace ArcTestsServices.Interfaces;

public interface IProductService
{
    Task<ProductDto?>GetProductAsync(int id);
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task CreateProductAsync(CreateProductRequest request);
    
}