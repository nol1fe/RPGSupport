namespace DataAccess.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RPGSupportDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RPGSupportDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Statistic.AddOrUpdate(s => s.Name,

                new Statistic { Id = 1, DefaultValue = 0, Name = "WW", FullName = "Walka wr�cz" },
                new Statistic { Id = 2, DefaultValue = 0, Name = "US", FullName = "Umiej�tno�ci strzeleckie" },
                new Statistic { Id = 3, DefaultValue = 0, Name = "K", FullName = "Krzepa" },
                new Statistic { Id = 4, DefaultValue = 0, Name = "Odp", FullName = "Odporno��" },
                new Statistic { Id = 5, DefaultValue = 0, Name = "Zr", FullName = "Zr�czno��" },
                new Statistic { Id = 6, DefaultValue = 0, Name = "Int", FullName = "Inteligencja" },
                new Statistic { Id = 7, DefaultValue = 0, Name = "SW", FullName = "Si�a woli" },
                new Statistic { Id = 8, DefaultValue = 0, Name = "Ogd", FullName = "Inteligencja" },
                new Statistic { Id = 9, DefaultValue = 0, Name = "A", FullName = "Inteligencja" },
                new Statistic { Id = 10, DefaultValue = 0, Name = "�yw", FullName = "�ywotno��" },
                new Statistic { Id = 11, DefaultValue = 0, Name = "S", FullName = "Si�a" },
                new Statistic { Id = 12, DefaultValue = 0, Name = "Wt", FullName = "Wytrzyma�o��" },
                new Statistic { Id = 13, DefaultValue = 0, Name = "Sz", FullName = "Szybko��" },
                new Statistic { Id = 14, DefaultValue = 0, Name = "Mag", FullName = "Magia" },
                new Statistic { Id = 15, DefaultValue = 0, Name = "PO", FullName = "Punkty ob��du" },
                new Statistic { Id = 16, DefaultValue = 0, Name = "PP", FullName = "Punkty przeznaczenia" }

                );

         }
    }
}
