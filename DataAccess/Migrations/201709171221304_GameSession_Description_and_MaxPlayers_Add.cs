namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameSession_Description_and_MaxPlayers_Add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameSessions", "Description", c => c.String());
            AddColumn("dbo.GameSessions", "MaximumPlayers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameSessions", "MaximumPlayers");
            DropColumn("dbo.GameSessions", "Description");
        }
    }
}
