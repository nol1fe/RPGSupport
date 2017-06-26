using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private RPGSupportDb _context;
        private DbSet<TEntity> _dbset;
        public Repository(RPGSupportDb context)
        {
            this._context = context;
            _dbset = _context.Set<TEntity>();
        }

        public List<TEntity> GetAll()
        {
            return _dbset.ToList();
        }
        public void Delete(TEntity entity)
        {
            _dbset.Attach(entity);
            _dbset.Remove(entity);
        }

        public void Edit(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public List<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbset.Find(id);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = StartQuery(includeProperties);
            return entities.FirstOrDefault(predicate);
        }

        public void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public List<TEntity> ToList(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = StartQuery(includeProperties);
            return entities.ToList();

        }

        public IQueryable<TEntity> StartQuery(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _dbset;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities.AsNoTracking();
        }

        public Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbset.Where(predicate).ToListAsync();
        }

        public Task<List<TEntity>> ToListAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = StartQuery(includeProperties);
            return entities.ToListAsync();
        }

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = StartQuery(includeProperties);
            return _dbset.FirstOrDefaultAsync(predicate);
        }
    }
}
