namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CurriculumTypes", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CurriculumTypes", "Name", c => c.String());
        }
    }
}
