
namespace CRMEngSystem.Data.Loaders.Core
{
    public interface IEntityDataLoader<TEntity> where TEntity : class
    {
        IQueryable<TEntity> LoadData(IQueryable<TEntity> query);
    }
}
