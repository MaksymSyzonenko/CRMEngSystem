using CRMEngSystem.Data.Repositories.Core;

namespace CRMEngSystem.Data.Repositories.Factory
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> Instantiate<TEntity>() where TEntity : class;
    }
}
