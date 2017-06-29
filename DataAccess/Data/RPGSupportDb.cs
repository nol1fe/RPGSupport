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
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<CharacterStatistic> CharacterStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameSession> GameSessions { get; set; }
        public DbSet<GameSessionSlot> GameSessionSlots { get; set; }
    }
}
