using System.Runtime.Remoting.Contexts;
using System.Data.Entity;
using Entities;

namespace DataAccess
{
    public class RPGSupportDb : DbContext
    {
        public RPGSupportDb()
            : base("RPGSupportDb")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }

    }
}
