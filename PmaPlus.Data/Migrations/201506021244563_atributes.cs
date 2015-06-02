namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atributes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerAttributes", "ClubId", c => c.Int(nullable: true));
            AddForeignKey("dbo.PlayerAttributes", "ClubId", "dbo.Clubs", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerAttributes", "ClubId", "dbo.Clubs");
            DropIndex("dbo.PlayerAttributes", new[] { "ClubId" });
            DropColumn("dbo.PlayerAttributes", "ClubId");
        }
    }
}
