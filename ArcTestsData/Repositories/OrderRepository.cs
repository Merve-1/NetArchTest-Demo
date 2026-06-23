using ArcTestsData.Entities;
using ArcTestsData.Interfaces;
//Order repo:
//1. Reside in ArcTestsData.Repositories namespace
//2. Implement IRepository
//3. Have name ending in Repository
namespace ArcTestsData.Repositories;

internal class OrderRepository: IRepository<OrderEntity>
{
    private readonly List<OrderEntity> _store = new();

    public Task<OrderEntity?> GetByIdAsync(int id)
        => Task.FromResult(_store.FirstOrDefault(o => o.Id == id));
    
    public Task<IEnumerable<OrderEntity>> GetAllAsync()
        => Task.FromResult<IEnumerable<OrderEntity>>(_store);

    public Task AddAsync(OrderEntity entity)
    {
        _store.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(OrderEntity entity) => Task.CompletedTask;

    public Task DeleteAsync(int id)
    {
        var item = _store.FirstOrDefault(o => o.Id == id);
        if (item is not null) _store.Remove(item);
        return Task.CompletedTask;
    }

}