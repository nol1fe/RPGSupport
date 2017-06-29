using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Interfaces.UnitOfWork;
using Interfaces.Repositories;
using System.Web;

namespace Services.Entity
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        private bool _isDisposed = false;

        public EntityService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public Task AddAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Insert(entity);
            return UnitOfWork.SaveChangesAsync();
        }
        public void Add(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Insert(entity);
            UnitOfWork.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            //doczytac o scope tranzakcyjnym dla operacji ''inwazyjnych'' na bazie danych
            using (UnitOfWork)
            {
                try {
                    UnitOfWork.Repository<TEntity>().Delete(entity);

                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex) {
                    UnitOfWork.Rollback();
                    // + exception handling
                }
            }
        }

        public void Delete(int id)
        {
            using (UnitOfWork)
            {
                try
                {
                    var entity = UnitOfWork.Repository<TEntity>().GetById(id);
                    UnitOfWork.Repository<TEntity>().Delete(entity);
                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    UnitOfWork.Rollback();
                    // + exception handling
                }
            }

        }

        public Task DeleteAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Delete(entity);
            return UnitOfWork.SaveChangesAsync();

        }

        public List<TEntity> GetAll()
        {
            return UnitOfWork.Repository<TEntity>().ToList();
        }

        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return UnitOfWork.Repository<TEntity>().ToList(includeProperties);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return UnitOfWork.Repository<TEntity>().ToListAsync();
        }

        public TEntity GetById(int id)
        {
            return UnitOfWork.Repository<TEntity>().GetById(id);
        }

        public void Update(TEntity entity)
        {
            using (UnitOfWork)
            {
                try
                {
                    UnitOfWork.Repository<TEntity>().Edit(entity);
                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    UnitOfWork.Rollback();
                }
            }
        }

        public Task UpdateAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Edit(entity);
            return UnitOfWork.SaveChangesAsync();

        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return UnitOfWork.Repository<TEntity>().GetSingle(predicate, includeProperties);
        }
        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return UnitOfWork.Repository<TEntity>().GetSingleAsync(predicate, includeProperties);
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            UnitOfWork.Dispose();
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }

    }
}
