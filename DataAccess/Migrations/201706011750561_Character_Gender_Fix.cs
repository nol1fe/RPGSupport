namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Character_Gender_Fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Characters", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Characters", "Gender", c => c.String());
        }
    }
}
