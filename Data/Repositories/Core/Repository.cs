using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using CRMEngSystem.Data.Context;
using CRMEngSystem.Data.Loaders.Core;

namespace CRMEngSystem.Data.Repositories.Core
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly CRMEngSystemDbContext _context;
        public Repository(CRMEngSystemDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddEntityAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            await _context.Set<TEntity>().AddAsync(entity);
            await Commit();

            return true;
        }

        public async Task<TEntity?> AddEntityGetAsync(TEntity entity)
        {
            if (entity == null)
                return null;

            var result = await _context.Set<TEntity>().AddAsync(entity);
            await Commit();

            return result.Entity;
        }

        public async Task<bool> UpdateEntityAsync<TKey>(TKey currentEntityId, TEntity updatedEntity)
        {
            if (updatedEntity == null)
                return false;

            var currentEntity = await _context.Set<TEntity>().FindAsync(currentEntityId);
            if (currentEntity == null)
                return false;

            _context.Entry(currentEntity).CurrentValues.SetValues(updatedEntity);
            await Commit();

            return true;
        }

        public async Task<bool> UpdateRangeAsync<TKey>(string keyName, IEnumerable<TKey> currentEntityIds, IEnumerable<TEntity> updatedEntities)
        {
            if (updatedEntities == null || !updatedEntities.Any() || currentEntityIds == null || !currentEntityIds.Any())
                return false;

            var currentEntities = await _context.Set<TEntity>().Where(e => currentEntityIds.Contains(EF.Property<TKey>(e, keyName))).ToListAsync();

            if (currentEntities.Count != updatedEntities.Count())
                return false;

            for (int i = 0; i < currentEntities.Count; i++)
            {
                _context.Entry(currentEntities[i]).CurrentValues.SetValues(updatedEntities.ElementAt(i));
            }

            await Commit();
            return true;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
                return false;

            _context.Set<TEntity>().UpdateRange(entities);
            await Commit();

            return true;
        }


        public async Task<bool> RemoveEntityAsync(TEntity entity)
        {
            if (entity == null)
                return false;

            _context.Set<TEntity>().Remove(entity);
            await Commit();

            return true;
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any())
                return false;

            _context.Set<TEntity>().RemoveRange(entities);
            await Commit();

            return true;
        }


        public async Task<TEntity?> GetEntityAsync<TKey>(IEntityDataLoader<TEntity> loader, Expression<Func<TEntity, TKey>> keySelector, TKey entityId)
            => await loader.LoadData(_context.Set<TEntity>().AsQueryable()).FirstOrDefaultAsync(Expression.Lambda<Func<TEntity, bool>>(Expression.Equal(keySelector.Body, Expression.Constant(entityId)), keySelector.Parameters));

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync(IEntityDataLoader<TEntity> loader)
            => await loader.LoadData(_context.Set<TEntity>().AsQueryable()).ToListAsync();

        public IQueryable<TEntity> GetAllEntitiesAsQueryable(IEntityDataLoader<TEntity> loader)
            => loader.LoadData(_context.Set<TEntity>().AsQueryable());

        public async Task Commit()
        {
            await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
