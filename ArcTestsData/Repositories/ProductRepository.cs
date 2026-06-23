using ArcTestsData.Entities;
using ArcTestsData.Interfaces;

namespace ArcTestsData.Repositories;
//Product Repo:
//1. Reside in ArcTestsData.Repositories namespace
//2. Implement IRepository
//3. Have name ending in Repository
internal class ProductRepository: IRepository<ProductEntity>
{
    private readonly List<ProductEntity> _store = new();

    public Task<ProductEntity?> GetByIdAsync(int id)
    {
        var product = _store.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }
    
    public Task<IEnumerable<ProductEntity>> GetAllAsync()
    => Task.FromResult<IEnumerable<ProductEntity>>(_store);

    public Task AddAsync(ProductEntity entity)
    {
        _store.Add(entity);
        return Task.CompletedTask;
    }
    
    public Task UpdateAsync(ProductEntity entity)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var item = _store.FirstOrDefault(p => p.Id == id);
        if (item is not null) _store.Remove(item);
        return Task.CompletedTask;
    }
}