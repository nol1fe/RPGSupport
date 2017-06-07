using System;
using System.Data.Entity.Migrations;

namespace DataAccess.Migrations
{

    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        GameSystem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CharacterStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatisticId = c.Int(nullable: false),
                        CharacterId = c.Int(nullable: false),
                        CurrentValue = c.Int(nullable: false),
                        GameSystem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.Statistics", t => t.StatisticId, cascadeDelete: true)
                .Index(t => t.StatisticId)
                .Index(t => t.CharacterId);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FullName = c.String(),
                        DefaultValue = c.Int(nullable: false),
                        GameSystem = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        LockoutEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Characters", "UserId", "dbo.Users");
            DropForeignKey("dbo.CharacterStatistics", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.CharacterStatistics", "CharacterId", "dbo.Characters");
            DropIndex("dbo.CharacterStatistics", new[] { "CharacterId" });
            DropIndex("dbo.CharacterStatistics", new[] { "StatisticId" });
            DropIndex("dbo.Characters", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Statistics");
            DropTable("dbo.CharacterStatistics");
            DropTable("dbo.Characters");
        }
    }
}
