using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();
        void Rollback();
    }
}
