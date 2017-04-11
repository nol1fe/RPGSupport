using DataAccess.Repositories;
using Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : RepositoryBase, IUnitOfWork
    {

   
  
        public UnitOfWork(/*RPGSupportDb database*/)
        {
            //if (database == null)
            //{
            //    throw new ArgumentNullException("database");
            //}

            //_database = database;
        }
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>();
        }


        public void Save()
        {
            Database.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Database.SaveChangesAsync();
        }

        //private bool _isDisposed = false;
        //public void Dispose()
        //{
        //    if (_isDisposed)
        //    {
        //        return;
        //    }

        //    _database.Dispose();

        //    _isDisposed = true;

        //    GC.SuppressFinalize(this);
        //}


    }
}
