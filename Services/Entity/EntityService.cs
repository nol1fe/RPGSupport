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
        //protected readonly IRepository<TEntity> _repository;
        private bool _isDisposed = false;


        public EntityService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            //_repository = UnitOfWork.Repository<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Insert(entity);
            //_repository.Insert(entity);
            return UnitOfWork.SaveChangesAsync();
        }
        public void Add(TEntity entity)
        {
            UnitOfWork.Repository<TEntity>().Insert(entity);

            //_repository.Insert(entity);
            UnitOfWork.SaveChanges();
        
        }

        public void Delete(TEntity entity)
        {
            //doczytac o scope tranzakcyjnym dla operacji ''inwazyjnych'' na bazie danych
            using (UnitOfWork)
            {
                try {
                    UnitOfWork.Repository<TEntity>().Delete(entity);

                    //_repository.Delete(entity);
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
                    //var entity = _repository.GetById(id);
                    var entity = UnitOfWork.Repository<TEntity>().GetById(id);
                    UnitOfWork.Repository<TEntity>().Delete(entity);
                    //_repository.Delete(entity);
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
            //_repository.Delete(entity);
            return UnitOfWork.SaveChangesAsync();

        }

        public List<TEntity> GetAll()
        {
            
            return UnitOfWork.Repository<TEntity>().ToList();
            //return _repository.ToList();
        }

        public List<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            //return _repository.ToList(includeProperties);
            return UnitOfWork.Repository<TEntity>().ToList(includeProperties);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            //return _repository.ToListAsync();
            return UnitOfWork.Repository<TEntity>().ToListAsync();

        }

        public TEntity GetById(int id)
        {
            return UnitOfWork.Repository<TEntity>().GetById(id);

            //return _repository.GetById(id);
        }

        public void Update(TEntity entity)
        {
            using (UnitOfWork)
            {
                try
                {
                    UnitOfWork.Repository<TEntity>().Edit(entity);

                    //_repository.Edit(entity);
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
            UnitOfWork.Repository<TEntity>().Edit(entity);

            //_repository.Edit(entity);
            return UnitOfWork.SaveChangesAsync();

        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return UnitOfWork.Repository<TEntity>().GetSingle(predicate, includeProperties);
            
            //return _repository.GetSingle(predicate, includeProperties);

        }
        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            //return _repository.GetSingleAsync(predicate, includeProperties);
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
