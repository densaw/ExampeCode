namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class somee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserDetail_Id", "dbo.UserDetails");
            DropForeignKey("dbo.UserDetails", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Clubs", "Address_Id", "dbo.Addresses");
            AddColumn("dbo.CurriculumTypes", "Name", c => c.String());
            AlterColumn("dbo.CurriculumTypes", "UsesBlocks", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForAttendance", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForObjectives", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForRatings", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForReports", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesWeeks", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForAttendance", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForObjectives", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForRatings", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForReports", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesSessions", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForAttendance", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForObjectives", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForRatings", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForReports", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.Users", "UserDetail_Id", "dbo.UserDetails", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserDetails", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Clubs", "Address_Id", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clubs", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.UserDetails", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Users", "UserDetail_Id", "dbo.UserDetails");
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForReports", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForRatings", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForObjectives", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesSessionsForAttendance", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesSessions", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForReports", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForRatings", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForObjectives", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesWeeksForAttendance", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesWeeks", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForReports", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForRatings", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForObjectives", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesBlocksForAttendance", c => c.Boolean());
            AlterColumn("dbo.CurriculumTypes", "UsesBlocks", c => c.Boolean());
            DropColumn("dbo.CurriculumTypes", "Name");
            AddForeignKey("dbo.Clubs", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.UserDetails", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Users", "UserDetail_Id", "dbo.UserDetails", "Id");
        }
    }
}
