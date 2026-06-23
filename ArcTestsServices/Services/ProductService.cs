using ArcTestsData.Entities;
using ArcTestsData.Interfaces;
using ArcTestsServices.DTOs;
using ArcTestsServices.Interfaces;

namespace ArcTestsServices.Services;
//Product Service - the only layer allowed to depend on the ArcTestsData.Repositories
//NetArchTest enforces that only ArcTestsServices can have a dependency on the data layer 
internal class ProductService: IProductService
{
    private readonly IRepository<ProductEntity> _productRepository;
    public ProductService(IRepository<ProductEntity> productRepository){
        _productRepository = productRepository;
        
    }

    public async Task<ProductDto?> GetProductAsync(int id)
    {
        var entity = await _productRepository.GetByIdAsync(id);
        if (entity is null) return null;

        return MapToDto(entity);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var entities = await _productRepository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    
    public async Task CreateProductAsync(CreateProductRequest request)
    {
        var entity = new ProductEntity(request.Name, request.Price, request.StockQuantity);
        await _productRepository.AddAsync(entity);
    }

    private static ProductDto MapToDto(ProductEntity e)
        => new(e.Id, e.Name, e.Price, e.StockQuantity);
}