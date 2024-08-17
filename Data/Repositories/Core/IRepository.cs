using System.Linq.Expressions;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Repositories.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> AddEntityAsync(TEntity entity);
        Task<TEntity?> AddEntityGetAsync(TEntity entity);
        Task<bool> UpdateEntityAsync<TKey>(TKey currentEntityId, TEntity updatedEntity);
        Task<bool> UpdateRangeAsync<TKey>(string keyName, IEnumerable<TKey> currentEntityIds, IEnumerable<TEntity> updatedEntities);
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> RemoveEntityAsync(TEntity entity);
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity?> GetEntityAsync<TKey>(IEntityDataLoader<TEntity> loader, Expression<Func<TEntity, TKey>> keySelector, TKey entityId);
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync(IEntityDataLoader<TEntity> loader);
        IQueryable<TEntity> GetAllEntitiesAsQueryable(IEntityDataLoader<TEntity> loader);
        Task Commit();
    }
}
