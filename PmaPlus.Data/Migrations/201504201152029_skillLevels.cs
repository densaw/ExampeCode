namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skillLevels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Skill_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Skills", t => t.Skill_Id)
                .Index(t => t.Skill_Id);
            
            AddColumn("dbo.Skills", "Name", c => c.String());
            AddColumn("dbo.Skills", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Aggression", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Anticipation", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Bravery", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Composure", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Concentration", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Creativity", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Decisions", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Determination", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Flair", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Influence", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "OffTheBall", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Positioning", c => c.Int(nullable: false));
            AddColumn("dbo.Skills", "Teamwork", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Crossing", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Dribling", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Finishing", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "FirstTouch", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "FreeKicks", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Heading", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Shooting", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "ThrowIns", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Marking", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Passing", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Penalty", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Tacking", c => c.Int(nullable: false));
            AlterColumn("dbo.Skills", "Technique", c => c.Int(nullable: false));
            DropColumn("dbo.Skills", "SkillName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "SkillName", c => c.String());
            DropForeignKey("dbo.SkillLevels", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.SkillLevels", new[] { "Skill_Id" });
            AlterColumn("dbo.Skills", "Technique", c => c.Int());
            AlterColumn("dbo.Skills", "Tacking", c => c.Int());
            AlterColumn("dbo.Skills", "Penalty", c => c.Int());
            AlterColumn("dbo.Skills", "Passing", c => c.Int());
            AlterColumn("dbo.Skills", "Marking", c => c.Int());
            AlterColumn("dbo.Skills", "ThrowIns", c => c.Int());
            AlterColumn("dbo.Skills", "Shooting", c => c.Int());
            AlterColumn("dbo.Skills", "Heading", c => c.Int());
            AlterColumn("dbo.Skills", "FreeKicks", c => c.Int());
            AlterColumn("dbo.Skills", "FirstTouch", c => c.Int());
            AlterColumn("dbo.Skills", "Finishing", c => c.Int());
            AlterColumn("dbo.Skills", "Dribling", c => c.Int());
            AlterColumn("dbo.Skills", "Crossing", c => c.Int());
            DropColumn("dbo.Skills", "Teamwork");
            DropColumn("dbo.Skills", "Positioning");
            DropColumn("dbo.Skills", "OffTheBall");
            DropColumn("dbo.Skills", "Influence");
            DropColumn("dbo.Skills", "Flair");
            DropColumn("dbo.Skills", "Determination");
            DropColumn("dbo.Skills", "Decisions");
            DropColumn("dbo.Skills", "Creativity");
            DropColumn("dbo.Skills", "Concentration");
            DropColumn("dbo.Skills", "Composure");
            DropColumn("dbo.Skills", "Bravery");
            DropColumn("dbo.Skills", "Anticipation");
            DropColumn("dbo.Skills", "Aggression");
            DropColumn("dbo.Skills", "Duration");
            DropColumn("dbo.Skills", "Name");
            DropTable("dbo.SkillLevels");
        }
    }
}
