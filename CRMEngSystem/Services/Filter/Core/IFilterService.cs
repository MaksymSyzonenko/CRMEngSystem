using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Services.Filter.Core
{
    public interface IFilterService<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Filter(IQueryable<TEntity> entities);
    }
}
