using Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryBase : IRepositoryBase
    {
        protected readonly RPGSupportDb Database;

        public RepositoryBase()
        {
            Database = new RPGSupportDb();
        }
        public void SaveChanges()
        {
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

    }
}
