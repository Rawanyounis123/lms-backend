using LearningManagementSystem.Domain.Entities;

namespace LearningManagementSystem.Application.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<(List<T> Items, long TotalCount)> GetPagedAsync(int page, int pageSize);
    Task<List<T>> GetByIdsAsync(IEnumerable<string> ids);
    Task<T?> GetByIdAsync(string id);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(string id, T entity);
    Task<bool> DeleteAsync(string id);
}
