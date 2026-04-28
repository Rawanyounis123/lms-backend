using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using MongoDB.Driver;

namespace LearningManagementSystem.Infrastructure.Repositories;

public abstract class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> Collection;

    protected MongoRepository(IMongoCollection<T> collection)
    {
        Collection = collection;
    }

    public async Task<List<T>> GetAllAsync() =>
        await Collection.Find(_ => true).ToListAsync();

    public async Task<(List<T> Items, long TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var filter = Builders<T>.Filter.Empty;
        var totalCount = await Collection.CountDocumentsAsync(filter);
        var items = await Collection.Find(filter)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<List<T>> GetByIdsAsync(IEnumerable<string> ids)
    {
        var filter = Builders<T>.Filter.In(x => x.Id, ids);
        return await Collection.Find(filter).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(string id) =>
        await Collection.Find(e => e.Id == id).FirstOrDefaultAsync();

    public async Task<T> CreateAsync(T entity)
    {
        await Collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<bool> UpdateAsync(string id, T entity)
    {
        var result = await Collection.ReplaceOneAsync(e => e.Id == id, entity);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await Collection.DeleteOneAsync(e => e.Id == id);
        return result.DeletedCount > 0;
    }
}
