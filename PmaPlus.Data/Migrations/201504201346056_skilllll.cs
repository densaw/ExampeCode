namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skilllll : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SkillLevels", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillLevels", new[] { "Skill_Id" });
            AddColumn("dbo.Skills", "SkillLevel_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Skills", "SkillLevel_Id");
            AddForeignKey("dbo.Skills", "SkillLevel_Id", "dbo.SkillLevels", "Id", cascadeDelete: true);
            DropColumn("dbo.SkillLevels", "Skill_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SkillLevels", "Skill_Id", c => c.Int());
            DropForeignKey("dbo.Skills", "SkillLevel_Id", "dbo.SkillLevels");
            DropIndex("dbo.Skills", new[] { "SkillLevel_Id" });
            DropColumn("dbo.Skills", "SkillLevel_Id");
            CreateIndex("dbo.SkillLevels", "Skill_Id");
            AddForeignKey("dbo.SkillLevels", "Skill_Id", "dbo.Skills", "Id");
        }
    }
}
