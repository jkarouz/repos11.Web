using repos11.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace repos11.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        ApplicationDbContext GetDbContext();
        IQueryable<TEntity> GetAll(bool includeInActive = false);
        Task<TEntity> Get(long id, bool includeInActive = false);
        Task<TEntity> Add(TEntity entity, long UserId);
        Task AddBacth(List<TEntity> entities, long UserId);
        Task<TEntity> Update(TEntity entity, long UserId);
        Task<int> UpdateBacth(List<TEntity> entities, long UserId);
        Task<TEntity> Delete(long id, long UserId);
        Task<int> DeleteBacth(IQueryable<TEntity> query, long UserId);
        Task<int> ForceDelete(long id);
        Task<int> ForceDeleteBatch(IQueryable<TEntity> query);
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
    }
}
