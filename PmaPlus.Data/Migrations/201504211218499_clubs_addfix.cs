namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clubs_addfix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDetails", "Birthday", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDetails", "Birthday", c => c.DateTime(storeType: "date"));
        }
    }
}
