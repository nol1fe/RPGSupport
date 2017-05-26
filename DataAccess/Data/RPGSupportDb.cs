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
        public DbSet<Character> Characters { get; set; }
        public DbSet<Statistic> Statistic { get; set; }
        public DbSet<CharacterStatistic> CharacterStatistic { get; set; }

    }
}
