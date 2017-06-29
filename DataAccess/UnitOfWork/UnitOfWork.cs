using DataAccess.Repositories;
using Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Repositories;
using System.Data.Entity;
using Entities;
using System.Web;
using Microsoft.AspNet.Identity;
using Interfaces;
using Autofac;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RPGSupportDb _context;
        private DbContextTransaction _transaction;
        private bool _isDisposed = false;


        public UnitOfWork(RPGSupportDb context)
        {
            _context = context ?? throw new ArgumentNullException("database");
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void SaveChanges()
        {
            TrackEntityBase();
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            TrackEntityBase();
            return await _context.SaveChangesAsync();
        }

        public void Rollback()
        {
            _context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        private void TrackEntityBase()
        {
            var entities = _context.ChangeTracker.Entries().Where(x => x.Entity is TrackedEntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            if (entities.Any())
            {
                var userId = HttpContext.Current.User.Identity.GetUserId<int>();

                foreach (var entity in entities)
                {
                    if (entity.State == EntityState.Added)
                    {
                        ((TrackedEntityBase)entity.Entity).Created = DateTime.UtcNow;
                        ((TrackedEntityBase)entity.Entity).CreatedByUserId = userId;
                    }

                    ((TrackedEntityBase)entity.Entity).Modified = DateTime.UtcNow;
                    ((TrackedEntityBase)entity.Entity).ModifiedByUserId = userId;
                }
            }
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
