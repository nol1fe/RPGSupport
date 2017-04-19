using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Interfaces.UnitOfWork;
using Interfaces.Repositories;

namespace Services.Entity
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        public IUnitOfWork UnitOfWork { get; private set; }
        protected readonly IRepository<TEntity> _repository;
        private bool _isDisposed = false;


        public EntityService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            _repository = UnitOfWork.Repository<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            _repository.Insert(entity);
            return UnitOfWork.SaveChangesAsync();
        }
        public void Add(TEntity entity)
        {
            _repository.Insert(entity);
            UnitOfWork.SaveChanges();

        }

        public void Delete(TEntity entity)
        {
            //doczytac o scope tranzakcyjnym dla operacji ''inwazyjnych'' na bazie danych
            using (UnitOfWork)
            {
                try {
                    _repository.Delete(entity);
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
                    var entity = _repository.GetById(id);
                    _repository.Delete(entity);
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
            _repository.Delete(entity);
            return UnitOfWork.SaveChangesAsync();

        }

        public List<TEntity> GetAll()
        {
            return _repository.ToList();
        }

        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.ToList(includeProperties);

        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _repository.ToListAsync();

        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(TEntity entity)
        {
            using (UnitOfWork)
            {
                try
                {
                    _repository.Edit(entity);
                    UnitOfWork.SaveChanges();

                }
                catch (Exception ex)
                {
                    UnitOfWork.Rollback();
                    // + exception handling
                }
            }
        }

        public Task UpdateAsync(TEntity entity)
        {
            _repository.Edit(entity);
            return UnitOfWork.SaveChangesAsync();

        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetSingle(predicate, includeProperties);

        }
        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.GetSingleAsync(predicate, includeProperties);

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
