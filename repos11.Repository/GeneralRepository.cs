using repos11.Repository.Entity;
using repos11.Repository.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace repos11.Repository
{
    public abstract class GeneralRepository<TEntity> : IGeneralRepository<TEntity>
        where TEntity : class, IEntity
    {
        public readonly ApplicationDbContext _context;

        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicationDbContext GetDbContext()
        {
            return _context;
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Where(expression);
        }

    }
}
