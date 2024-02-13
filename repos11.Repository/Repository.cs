using N.EntityFramework.Extensions;
using repos11.Repository.Entity;
using repos11.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace repos11.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext GetDbContext()
        {
            return _context;
        }

        public async Task<TEntity> Add(TEntity entity, long UserId)
        {
            entity.CreatedBy = (int)UserId;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = null;
            entity.UpdatedDate = null;
            entity.IsActive = true;

            _context.Set<TEntity>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task AddBacth(List<TEntity> entities, long UserId)
        {
            var now = DateTime.Now;
            entities.ForEach(item =>
            {
                item.CreatedBy = (int)UserId;
                item.CreatedDate = now;
                item.UpdatedBy = null;
                item.UpdatedDate = null;
                item.IsActive = true;
            });

            await _context.BulkInsertAsync(entities);
        }

        public async Task<TEntity> Update(TEntity entity, long UserId)
        {
            entity.IsActive = true;
            entity.UpdatedBy = (int)UserId;
            entity.UpdatedDate = DateTime.Now;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> UpdateBacth(List<TEntity> entities, long UserId)
        {
            var now = DateTime.Now;
            entities.ForEach(item =>
            {
                item.UpdatedBy = (int)UserId;
                item.UpdatedDate = now;
            });

            return await _context.BulkUpdateAsync(entities);
        }

        public async Task<TEntity> Delete(long id, long UserId)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            entity.IsActive = false;
            entity.UpdatedBy = (int)UserId;
            entity.UpdatedDate = DateTime.Now;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<int> DeleteBacth(IQueryable<TEntity> query, long UserId)
        {
            var entities = (await query.ToListAsync());
            entities.ForEach(f =>
            {
                f.UpdatedBy = (int)UserId;
                f.UpdatedDate = DateTime.Now;
                f.IsActive = false;
            });

            return await _context.BulkUpdateAsync(entities);
        }

        public async Task<int> ForceDelete(long id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return 0;
            }

            _context.Set<TEntity>().Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> ForceDeleteBatch(IQueryable<TEntity> query)
        {
            return await query.DeleteFromQueryAsync();
        }

        public async Task<TEntity> Get(long id, bool includeInActive = false)
        {
            if (includeInActive)
                return await _context.Set<TEntity>().Where(w => w.Id == id).FirstOrDefaultAsync();
            else
                return await _context.Set<TEntity>().Where(w => w.IsActive && w.Id == id).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetAll(bool includeInActive = true)
        {
            if (includeInActive)
                return _context.Set<TEntity>().AsNoTracking();
            else
                return _context.Set<TEntity>().Where(w => w.IsActive).AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

    }
}
