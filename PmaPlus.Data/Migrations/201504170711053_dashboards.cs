namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dashboards : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clubs", "CreateAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clubs", "CreateAt");
        }
    }
}
