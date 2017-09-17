namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameSession_GameId_Add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GameSessions", "Game_Id", "dbo.Games");
            DropIndex("dbo.GameSessions", new[] { "Game_Id" });
            RenameColumn(table: "dbo.GameSessions", name: "Game_Id", newName: "GameId");
            AlterColumn("dbo.GameSessions", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.GameSessions", "GameId");
            AddForeignKey("dbo.GameSessions", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameSessions", "GameId", "dbo.Games");
            DropIndex("dbo.GameSessions", new[] { "GameId" });
            AlterColumn("dbo.GameSessions", "GameId", c => c.Int());
            RenameColumn(table: "dbo.GameSessions", name: "GameId", newName: "Game_Id");
            CreateIndex("dbo.GameSessions", "Game_Id");
            AddForeignKey("dbo.GameSessions", "Game_Id", "dbo.Games", "Id");
        }
    }
}
