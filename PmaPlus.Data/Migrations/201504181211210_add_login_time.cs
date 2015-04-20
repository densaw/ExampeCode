namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_login_time : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LoggedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Clubs", "ColorTheme", c => c.String());
            AddColumn("dbo.Clubs", "Background", c => c.String());
            AddColumn("dbo.Players", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "CreateAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "UpdateAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.FACourses", "Duration", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.FACourses", "Duration", c => c.Int());
            AlterColumn("dbo.Users", "UpdateAt", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Users", "CreateAt", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("dbo.Players", "Status");
            DropColumn("dbo.Clubs", "Background");
            DropColumn("dbo.Clubs", "ColorTheme");
            DropColumn("dbo.Users", "LoggedAt");
        }
    }
}
