namespace ArcTestsData.Interfaces;
//Generic repository interface, all repositories in ArcTestsData. Repositories must implement this 
internal interface IRepository<T> where T: class
{
    Task<T?> GetByIdAsync(int id);
    
    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(int id);
}