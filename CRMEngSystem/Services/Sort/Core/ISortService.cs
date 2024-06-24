using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Services.Sort.Core
{
    public interface ISortService<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Sort(IQueryable<TEntity> entities);
    }
}
