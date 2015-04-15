namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserDetail_UserDetailId", "dbo.UserDetails");
            DropPrimaryKey("dbo.UserDetails");
            DropColumn("dbo.UserDetails", "UserDetailId");
            RenameColumn(table: "dbo.Users", name: "UserDetail_UserDetailId", newName: "UserDetail_Id");
            RenameIndex(table: "dbo.Users", name: "IX_UserDetail_UserDetailId", newName: "IX_UserDetail_Id");
            AddColumn("dbo.UserDetails", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserDetails", "Id");
            AddForeignKey("dbo.Users", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserDetails", "UserDetailId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Users", "UserDetail_Id", "dbo.UserDetails");
            DropPrimaryKey("dbo.UserDetails");
            DropColumn("dbo.UserDetails", "Id");
            AddPrimaryKey("dbo.UserDetails", "UserDetailId");
            RenameIndex(table: "dbo.Users", name: "IX_UserDetail_Id", newName: "IX_UserDetail_UserDetailId");
            RenameColumn(table: "dbo.Users", name: "UserDetail_Id", newName: "UserDetail_UserDetailId");
            AddForeignKey("dbo.Users", "UserDetail_UserDetailId", "dbo.UserDetails", "UserDetailId");
        }
    }
}
