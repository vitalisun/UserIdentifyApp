namespace IndentifyApp.DAL;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(T entity);
}