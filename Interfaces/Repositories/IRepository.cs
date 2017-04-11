using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(int id);
        //IQueryable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll();

        void Edit(TEntity entity);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
    }
}
