using DataAccess.Repositories;
using Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using System.Data.Entity;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork :  IUnitOfWork
    {
        private readonly RPGSupportDb _context;
        private DbContextTransaction _transaction;
        private bool _isDisposed = false;


        public UnitOfWork(RPGSupportDb context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("database");
            }

            _context = context;
        }


        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();

        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }


        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _context.Dispose();

            _isDisposed = true;

            GC.SuppressFinalize(this);
        }


    }
}
