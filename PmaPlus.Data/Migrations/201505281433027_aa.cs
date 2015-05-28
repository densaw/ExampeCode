namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Scenarios", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.ScenarioSessions", "ScenarioId", "dbo.Scenarios");
            DropForeignKey("dbo.ScenarioSessions", "SessionId", "dbo.Sessions");
            DropIndex("dbo.Scenarios", new[] { "Session_Id" });
            DropIndex("dbo.ScenarioSessions", new[] { "SessionId" });
            DropIndex("dbo.ScenarioSessions", new[] { "ScenarioId" });
            DropTable("dbo.ScenarioSessions");
            CreateTable(
                "dbo.ScenarioSessions",
                c => new
                    {
                        Scenario_Id = c.Int(nullable: false),
                        Session_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Scenario_Id, t.Session_Id })
                .ForeignKey("dbo.Scenarios", t => t.Scenario_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sessions", t => t.Session_Id, cascadeDelete: true)
                .Index(t => t.Scenario_Id)
                .Index(t => t.Session_Id);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ScenarioSessions",
                c => new
                    {
                        SessionId = c.Int(nullable: false),
                        ScenarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SessionId, t.ScenarioId });
            
            AddColumn("dbo.Scenarios", "Session_Id", c => c.Int());
            DropForeignKey("dbo.ScenarioSessions", "Session_Id", "dbo.Sessions");
            DropForeignKey("dbo.ScenarioSessions", "Scenario_Id", "dbo.Scenarios");
            DropIndex("dbo.ScenarioSessions", new[] { "Session_Id" });
            DropIndex("dbo.ScenarioSessions", new[] { "Scenario_Id" });
            DropTable("dbo.ScenarioSessions");
            CreateIndex("dbo.ScenarioSessions", "ScenarioId");
            CreateIndex("dbo.ScenarioSessions", "SessionId");
            CreateIndex("dbo.Scenarios", "Session_Id");
            AddForeignKey("dbo.ScenarioSessions", "SessionId", "dbo.Sessions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ScenarioSessions", "ScenarioId", "dbo.Scenarios", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Scenarios", "Session_Id", "dbo.Sessions", "Id");
        }
    }
}
