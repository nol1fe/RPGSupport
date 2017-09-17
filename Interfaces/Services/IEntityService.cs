using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Interfaces.Services
{
    public interface IEntityService<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Add(TEntity entity);

        void Delete(TEntity entity);
        void Delete(int id);
        Task DeleteAsync(TEntity entity);
        List<TEntity> GetAll();
        List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetAllAsync();
        TEntity GetById(int id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);

    }
}
