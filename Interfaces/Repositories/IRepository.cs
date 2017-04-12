using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        List<TEntity> ToList(params Expression<Func<TEntity, object>>[] includeProperties);
        List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetAll();
    
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity GetById(int id);

        void Edit(TEntity entity);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> StartQuery(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> ToListAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);


    }
}
