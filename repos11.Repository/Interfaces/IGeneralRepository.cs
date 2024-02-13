using repos11.Repository.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace repos11.Repository.Interfaces
{
    public interface IGeneralRepository<TEntity> where TEntity : class
    {
        ApplicationDbContext GetDbContext();
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
    }
}
