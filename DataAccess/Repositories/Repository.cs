using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Metadata;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : RepositoryBase, IRepository<TEntity> where TEntity : class
    {

        private DbSet<TEntity> dbset;
        public Repository()

        {
            dbset = Database.Set<TEntity>();
        }
        public TEntity GetById(int id)
        {
            return dbset.Find(id);
        }
        public void Delete(TEntity entity)
        {
            Database.Entry(entity).State = EntityState.Deleted;
        }

        public void Edit(TEntity entity)
        {
            Database.Entry(entity).State = EntityState.Modified;
        }

        //public IQueryable<TEntity> GetAll()
        //{
        //    return dbset;
        //}

        public void Insert(TEntity entity)
        {
            dbset.Add(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbset.ToList();
        }
    }
}
