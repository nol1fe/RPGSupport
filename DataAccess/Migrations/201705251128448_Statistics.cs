namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Statistics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Statistics", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Statistics", "FullName");
        }
    }
}
