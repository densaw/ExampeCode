namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_createAt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "CreateAt", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Users", "UpdateAt", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "UpdateAt", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Users", "CreateAt", c => c.DateTime(storeType: "date"));
        }
    }
}
