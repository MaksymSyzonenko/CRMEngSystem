using CRMEngSystem.Data.Entities.Core;

namespace CRMEngSystem.Services.Search.Core
{
    public interface ISearchService<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> Search(IQueryable<TEntity> entities);
    }
}
