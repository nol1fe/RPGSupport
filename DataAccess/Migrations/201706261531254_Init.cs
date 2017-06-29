namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
                        GameSystem = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Int(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        TrackedEntityStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId, cascadeDelete: false)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId);
            
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
                        TrackedEntityStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CharacterStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentValue = c.Int(nullable: false),
                        StatisticId = c.Int(nullable: false),
                        CharacterId = c.Int(nullable: false),
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
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        GameSystem = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Int(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        TrackedEntityStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId, cascadeDelete: false)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.GameSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GameSessionState = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        ModifiedByUserId = c.Int(nullable: false),
                        CreatedByUserId = c.Int(nullable: false),
                        TrackedEntityStatus = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ModifiedByUserId, cascadeDelete: false)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.ModifiedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.GameSessionSlots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SlotStatus = c.Int(nullable: false),
                        GameSessionId = c.Int(nullable: false),
                        CharacterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Characters", t => t.CharacterId, cascadeDelete: true)
                .ForeignKey("dbo.GameSessions", t => t.GameSessionId, cascadeDelete: true)
                .Index(t => t.GameSessionId)
                .Index(t => t.CharacterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.GameSessions", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GameSessions", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.GameSessionSlots", "GameSessionId", "dbo.GameSessions");
            DropForeignKey("dbo.GameSessionSlots", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.GameSessions", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Games", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.CharacterStatistics", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.CharacterStatistics", "CharacterId", "dbo.Characters");
            DropForeignKey("dbo.Characters", "ModifiedByUserId", "dbo.Users");
            DropForeignKey("dbo.Characters", "CreatedByUserId", "dbo.Users");
            DropIndex("dbo.GameSessionSlots", new[] { "CharacterId" });
            DropIndex("dbo.GameSessionSlots", new[] { "GameSessionId" });
            DropIndex("dbo.GameSessions", new[] { "Game_Id" });
            DropIndex("dbo.GameSessions", new[] { "CreatedByUserId" });
            DropIndex("dbo.GameSessions", new[] { "ModifiedByUserId" });
            DropIndex("dbo.Games", new[] { "CreatedByUserId" });
            DropIndex("dbo.Games", new[] { "ModifiedByUserId" });
            DropIndex("dbo.CharacterStatistics", new[] { "CharacterId" });
            DropIndex("dbo.CharacterStatistics", new[] { "StatisticId" });
            DropIndex("dbo.Characters", new[] { "CreatedByUserId" });
            DropIndex("dbo.Characters", new[] { "ModifiedByUserId" });
            DropTable("dbo.GameSessionSlots");
            DropTable("dbo.GameSessions");
            DropTable("dbo.Games");
            DropTable("dbo.Statistics");
            DropTable("dbo.CharacterStatistics");
            DropTable("dbo.Users");
            DropTable("dbo.Characters");
        }
    }
}
