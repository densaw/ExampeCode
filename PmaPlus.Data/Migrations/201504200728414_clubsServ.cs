namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clubsServ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "PostCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Clubs", "Established", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clubs", "Established", c => c.Int());
            AlterColumn("dbo.Addresses", "PostCode", c => c.Int());
        }
    }
}
