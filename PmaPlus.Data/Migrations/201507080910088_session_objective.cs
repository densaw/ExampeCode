namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class session_objective : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerObjectives", "StartSessionResultId", "dbo.SessionResults");
            DropIndex("dbo.PlayerObjectives", new[] { "StartSessionResultId" });
            AlterColumn("dbo.PlayerObjectives", "StartSessionResultId", c => c.Int(nullable: false));
            CreateIndex("dbo.PlayerObjectives", "StartSessionResultId");
            AddForeignKey("dbo.PlayerObjectives", "StartSessionResultId", "dbo.SessionResults", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerObjectives", "StartSessionResultId", "dbo.SessionResults");
            DropIndex("dbo.PlayerObjectives", new[] { "StartSessionResultId" });
            AlterColumn("dbo.PlayerObjectives", "StartSessionResultId", c => c.Int());
            CreateIndex("dbo.PlayerObjectives", "StartSessionResultId");
            AddForeignKey("dbo.PlayerObjectives", "StartSessionResultId", "dbo.SessionResults", "Id");
        }
    }
}
