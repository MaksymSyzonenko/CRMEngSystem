using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Repositories.Core;

namespace CRMEngSystem.Data.Repositories.Factory
{
    public sealed class RepositoryFactory : IRepositoryFactory
    {
        private readonly CRMEngSystemDbContext _context;
        public RepositoryFactory(CRMEngSystemDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Instantiate<TEntity>() where TEntity : class
            => new Repository<TEntity>(_context);
    }
}
