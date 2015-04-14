namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inialise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Telephone = c.String(),
                        Mobile = c.String(),
                        TownCity = c.String(),
                        PostCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserDetailId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.UserDetailId)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                        UserDetail_UserDetailId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserDetails", t => t.UserDetail_UserDetailId)
                .Index(t => t.UserDetail_UserDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserDetail_UserDetailId", "dbo.UserDetails");
            DropForeignKey("dbo.UserDetails", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "UserDetail_UserDetailId" });
            DropIndex("dbo.UserDetails", new[] { "Address_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UserDetails");
            DropTable("dbo.Addresses");
        }
    }
}
