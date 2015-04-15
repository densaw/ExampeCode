namespace PmaPlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigbase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BodyParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(),
                        InjuryName = c.String(),
                        Description = c.String(),
                        Picture = c.String(),
                        Treatment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseType = c.Int(),
                        CertificateName = c.String(),
                        CertificateNumber = c.Int(),
                        PassDate = c.DateTime(storeType: "date"),
                        ExpiryDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chairmen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 50),
                        Telephone = c.String(maxLength: 12),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClubAdmins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Clubs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Logo = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        Established = c.Int(),
                        Address_Id = c.Int(),
                        Chairman_Id = c.Int(),
                        ClubAdmin_Id = c.Int(),
                        Physiotherapist_Id = c.Int(),
                        WelfareOfficer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Chairmen", t => t.Chairman_Id)
                .ForeignKey("dbo.ClubAdmins", t => t.ClubAdmin_Id)
                .ForeignKey("dbo.Physiotherapists", t => t.Physiotherapist_Id)
                .ForeignKey("dbo.WelfareOfficers", t => t.WelfareOfficer_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.Chairman_Id)
                .Index(t => t.ClubAdmin_Id)
                .Index(t => t.Physiotherapist_Id)
                .Index(t => t.WelfareOfficer_Id);
            
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(),
                        Club_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.TeamId)
                .Index(t => t.Club_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Club_Id = c.Int(),
                        Curriculum_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id)
                .ForeignKey("dbo.Curricula", t => t.Curriculum_Id)
                .Index(t => t.Club_Id)
                .Index(t => t.Curriculum_Id);
            
            CreateTable(
                "dbo.Curricula",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AgeGroup = c.Int(),
                        NumberOfBlocks = c.Int(),
                        NumberOfWeeks = c.Int(),
                        NumberOfSessions = c.Int(),
                        CurriculumDetail_Id = c.Int(),
                        CurriculumType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CurriculumDetails", t => t.CurriculumDetail_Id)
                .ForeignKey("dbo.CurriculumTypes", t => t.CurriculumType_Id)
                .Index(t => t.CurriculumDetail_Id)
                .Index(t => t.CurriculumType_Id);
            
            CreateTable(
                "dbo.CurriculumBlocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Curriculum_Id = c.Int(),
                        CurriculumDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curricula", t => t.Curriculum_Id)
                .ForeignKey("dbo.CurriculumDetails", t => t.CurriculumDetail_Id)
                .Index(t => t.Curriculum_Id)
                .Index(t => t.CurriculumDetail_Id);
            
            CreateTable(
                "dbo.CurriculumDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Number = c.String(),
                        CoachDescription = c.String(),
                        PlayersFriendlyName = c.String(),
                        PlayersFriendlyPicture = c.String(),
                        PlayersDescription = c.String(),
                        Coach_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coaches", t => t.Coach_Id)
                .Index(t => t.Coach_Id);
            
            CreateTable(
                "dbo.CurriculumWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurriculumBlock_Id = c.Int(),
                        CurriculumDetail_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CurriculumBlocks", t => t.CurriculumBlock_Id)
                .ForeignKey("dbo.CurriculumDetails", t => t.CurriculumDetail_Id)
                .Index(t => t.CurriculumBlock_Id)
                .Index(t => t.CurriculumDetail_Id);
            
            CreateTable(
                "dbo.CurriculumTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsesBlocks = c.Boolean(),
                        UsesBlocksForAttendance = c.Boolean(),
                        UsesBlocksForObjectives = c.Boolean(),
                        UsesBlocksForRatings = c.Boolean(),
                        UsesBlocksForReports = c.Boolean(),
                        UsesWeeks = c.Boolean(),
                        UsesWeeksForAttendance = c.Boolean(),
                        UsesWeeksForObjectives = c.Boolean(),
                        UsesWeeksForRatings = c.Boolean(),
                        UsesWeeksForReports = c.Boolean(),
                        UsesSessions = c.Boolean(),
                        UsesSessionsForAttendance = c.Boolean(),
                        UsesSessionsForObjectives = c.Boolean(),
                        UsesSessionsForRatings = c.Boolean(),
                        UsesSessionsForReports = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(storeType: "date"),
                        OppositionTeam = c.String(),
                        GoalsFor = c.Int(),
                        GoalsAway = c.Int(),
                        Type = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Club_Id = c.Int(),
                        Team_Id = c.Int(),
                        Team1_Id = c.Int(),
                        User_Id = c.Int(),
                        Team_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .ForeignKey("dbo.Teams", t => t.Team1_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id1)
                .Index(t => t.Club_Id)
                .Index(t => t.Team_Id)
                .Index(t => t.Team1_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Team_Id1);
            
            CreateTable(
                "dbo.PlayerInjuries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InjuryDate = c.DateTime(storeType: "date"),
                        InjuryName = c.String(),
                        Stage = c.String(),
                        RecoveryDate = c.DateTime(),
                        BodyPart_Id = c.Int(),
                        Player_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BodyParts", t => t.BodyPart_Id)
                .ForeignKey("dbo.Players", t => t.Player_Id)
                .Index(t => t.BodyPart_Id)
                .Index(t => t.Player_Id);
            
            CreateTable(
                "dbo.HeadOfEducations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Club_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Club_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Physiotherapists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.SportScientists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Club_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clubs", t => t.Club_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Club_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.WelfareOfficers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.FACourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Duration = c.Int(),
                        Descriprion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HeadOfAcademies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NutritionAlternatives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoLink = c.String(),
                        NutritionFoodTypeAlternative_Id = c.Int(),
                        NutritionFoodTypeBadItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NutritionFoodTypes", t => t.NutritionFoodTypeAlternative_Id)
                .ForeignKey("dbo.NutritionFoodTypes", t => t.NutritionFoodTypeBadItem_Id)
                .Index(t => t.NutritionFoodTypeAlternative_Id)
                .Index(t => t.NutritionFoodTypeBadItem_Id);
            
            CreateTable(
                "dbo.NutritionFoodTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        FoodName = c.String(),
                        Mealtime = c.Int(),
                        GoodFor = c.String(),
                        PortionSize = c.String(),
                        Description = c.String(),
                        Picture = c.String(),
                        Calories = c.String(),
                        CaloriesFromFat = c.String(),
                        TotalFat = c.String(),
                        SaturatedFat = c.String(),
                        TransFat = c.String(),
                        Cholesterol = c.String(),
                        Sodium = c.String(),
                        Potassium = c.String(),
                        TotalCrbohydrate = c.String(),
                        DietaryFibre = c.String(),
                        Sugars = c.String(),
                        Protein = c.String(),
                        VitaminA = c.String(),
                        VitaminB = c.String(),
                        VitaminC = c.String(),
                        VitaminD = c.String(),
                        VitaminE = c.String(),
                        Iron = c.String(),
                        Calcium = c.String(),
                        DailyIntake = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhysiotherapyExercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Videolink = c.String(),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerAttributes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Boolean(),
                        MaxScore = c.Int(),
                        AgeFrom = c.Int(),
                        AgeTo = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Scouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                        VideoLink = c.String(),
                        Description = c.String(),
                        BallControll = c.Int(nullable: false),
                        Corners = c.Int(nullable: false),
                        Crossing = c.Int(),
                        Dribling = c.Int(),
                        Finishing = c.Int(),
                        FirstTouch = c.Int(),
                        FreeKicks = c.Int(),
                        Heading = c.Int(),
                        Shooting = c.Int(),
                        ThrowIns = c.Int(),
                        Marking = c.Int(),
                        Passing = c.Int(),
                        Penalty = c.Int(),
                        Tacking = c.Int(),
                        Technique = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SportsScienceExercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Measure = c.String(),
                        LowValue = c.String(),
                        HightValue = c.String(),
                        NationalAverage = c.String(),
                        Video = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SportsScienceTests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TestType = c.Int(),
                        TestName = c.String(),
                        TestDescription = c.String(),
                        ZScoreFormula = c.String(),
                        Measure = c.String(),
                        LowValue = c.String(),
                        HightValue = c.String(),
                        NationalAverage = c.String(),
                        TestVideo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserDetails", "AboutMe", c => c.String());
            AddColumn("dbo.UserDetails", "FaNumber", c => c.Int());
            AddColumn("dbo.UserDetails", "ProfilePicture", c => c.String());
            AddColumn("dbo.UserDetails", "Nationality", c => c.String());
            AddColumn("dbo.Users", "CreateAt", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.Users", "UpdateAt", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Addresses", "Address1", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Addresses", "Address2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Addresses", "Address3", c => c.String(maxLength: 50));
            AlterColumn("dbo.Addresses", "Telephone", c => c.String(maxLength: 12));
            AlterColumn("dbo.Addresses", "Mobile", c => c.String(maxLength: 50));
            AlterColumn("dbo.Addresses", "TownCity", c => c.String(maxLength: 50));
            AlterColumn("dbo.Addresses", "PostCode", c => c.Int());
            AlterColumn("dbo.UserDetails", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserDetails", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserDetails", "Birthday", c => c.DateTime(storeType: "date"));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Password", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Scouts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.NutritionAlternatives", "NutritionFoodTypeBadItem_Id", "dbo.NutritionFoodTypes");
            DropForeignKey("dbo.NutritionAlternatives", "NutritionFoodTypeAlternative_Id", "dbo.NutritionFoodTypes");
            DropForeignKey("dbo.WelfareOfficers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Clubs", "WelfareOfficer_Id", "dbo.WelfareOfficers");
            DropForeignKey("dbo.SportScientists", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SportScientists", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Physiotherapists", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Clubs", "Physiotherapist_Id", "dbo.Physiotherapists");
            DropForeignKey("dbo.HeadOfEducations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.HeadOfEducations", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Coaches", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Players", "Team_Id1", "dbo.Teams");
            DropForeignKey("dbo.Players", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Players", "Team1_Id", "dbo.Teams");
            DropForeignKey("dbo.Players", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.PlayerInjuries", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.PlayerInjuries", "BodyPart_Id", "dbo.BodyParts");
            DropForeignKey("dbo.Players", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Matches", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Curriculum_Id", "dbo.Curricula");
            DropForeignKey("dbo.Curricula", "CurriculumType_Id", "dbo.CurriculumTypes");
            DropForeignKey("dbo.CurriculumWeeks", "CurriculumDetail_Id", "dbo.CurriculumDetails");
            DropForeignKey("dbo.CurriculumWeeks", "CurriculumBlock_Id", "dbo.CurriculumBlocks");
            DropForeignKey("dbo.Curricula", "CurriculumDetail_Id", "dbo.CurriculumDetails");
            DropForeignKey("dbo.CurriculumBlocks", "CurriculumDetail_Id", "dbo.CurriculumDetails");
            DropForeignKey("dbo.CurriculumDetails", "Coach_Id", "dbo.Coaches");
            DropForeignKey("dbo.CurriculumBlocks", "Curriculum_Id", "dbo.Curricula");
            DropForeignKey("dbo.Coaches", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Coaches", "Club_Id", "dbo.Clubs");
            DropForeignKey("dbo.Clubs", "ClubAdmin_Id", "dbo.ClubAdmins");
            DropForeignKey("dbo.Clubs", "Chairman_Id", "dbo.Chairmen");
            DropForeignKey("dbo.Clubs", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.ClubAdmins", "User_Id", "dbo.Users");
            DropIndex("dbo.Scouts", new[] { "User_Id" });
            DropIndex("dbo.NutritionAlternatives", new[] { "NutritionFoodTypeBadItem_Id" });
            DropIndex("dbo.NutritionAlternatives", new[] { "NutritionFoodTypeAlternative_Id" });
            DropIndex("dbo.WelfareOfficers", new[] { "User_Id" });
            DropIndex("dbo.SportScientists", new[] { "User_Id" });
            DropIndex("dbo.SportScientists", new[] { "Club_Id" });
            DropIndex("dbo.Physiotherapists", new[] { "User_Id" });
            DropIndex("dbo.HeadOfEducations", new[] { "User_Id" });
            DropIndex("dbo.HeadOfEducations", new[] { "Club_Id" });
            DropIndex("dbo.PlayerInjuries", new[] { "Player_Id" });
            DropIndex("dbo.PlayerInjuries", new[] { "BodyPart_Id" });
            DropIndex("dbo.Players", new[] { "Team_Id1" });
            DropIndex("dbo.Players", new[] { "User_Id" });
            DropIndex("dbo.Players", new[] { "Team1_Id" });
            DropIndex("dbo.Players", new[] { "Team_Id" });
            DropIndex("dbo.Players", new[] { "Club_Id" });
            DropIndex("dbo.Matches", new[] { "Team_Id" });
            DropIndex("dbo.CurriculumWeeks", new[] { "CurriculumDetail_Id" });
            DropIndex("dbo.CurriculumWeeks", new[] { "CurriculumBlock_Id" });
            DropIndex("dbo.CurriculumDetails", new[] { "Coach_Id" });
            DropIndex("dbo.CurriculumBlocks", new[] { "CurriculumDetail_Id" });
            DropIndex("dbo.CurriculumBlocks", new[] { "Curriculum_Id" });
            DropIndex("dbo.Curricula", new[] { "CurriculumType_Id" });
            DropIndex("dbo.Curricula", new[] { "CurriculumDetail_Id" });
            DropIndex("dbo.Teams", new[] { "Curriculum_Id" });
            DropIndex("dbo.Teams", new[] { "Club_Id" });
            DropIndex("dbo.Coaches", new[] { "User_Id" });
            DropIndex("dbo.Coaches", new[] { "Club_Id" });
            DropIndex("dbo.Coaches", new[] { "TeamId" });
            DropIndex("dbo.Clubs", new[] { "WelfareOfficer_Id" });
            DropIndex("dbo.Clubs", new[] { "Physiotherapist_Id" });
            DropIndex("dbo.Clubs", new[] { "ClubAdmin_Id" });
            DropIndex("dbo.Clubs", new[] { "Chairman_Id" });
            DropIndex("dbo.Clubs", new[] { "Address_Id" });
            DropIndex("dbo.ClubAdmins", new[] { "User_Id" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.UserDetails", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserDetails", "LastName", c => c.String());
            AlterColumn("dbo.UserDetails", "FirstName", c => c.String());
            AlterColumn("dbo.Addresses", "PostCode", c => c.Int(nullable: false));
            AlterColumn("dbo.Addresses", "TownCity", c => c.String());
            AlterColumn("dbo.Addresses", "Mobile", c => c.String());
            AlterColumn("dbo.Addresses", "Telephone", c => c.String());
            AlterColumn("dbo.Addresses", "Address3", c => c.String());
            AlterColumn("dbo.Addresses", "Address2", c => c.String());
            AlterColumn("dbo.Addresses", "Address1", c => c.String());
            DropColumn("dbo.Users", "UpdateAt");
            DropColumn("dbo.Users", "CreateAt");
            DropColumn("dbo.UserDetails", "Nationality");
            DropColumn("dbo.UserDetails", "ProfilePicture");
            DropColumn("dbo.UserDetails", "FaNumber");
            DropColumn("dbo.UserDetails", "AboutMe");
            DropTable("dbo.SportsScienceTests");
            DropTable("dbo.SportsScienceExercises");
            DropTable("dbo.Skills");
            DropTable("dbo.Scouts");
            DropTable("dbo.PlayerAttributes");
            DropTable("dbo.PhysiotherapyExercises");
            DropTable("dbo.NutritionFoodTypes");
            DropTable("dbo.NutritionAlternatives");
            DropTable("dbo.HeadOfAcademies");
            DropTable("dbo.FACourses");
            DropTable("dbo.WelfareOfficers");
            DropTable("dbo.SportScientists");
            DropTable("dbo.Physiotherapists");
            DropTable("dbo.HeadOfEducations");
            DropTable("dbo.PlayerInjuries");
            DropTable("dbo.Players");
            DropTable("dbo.Matches");
            DropTable("dbo.CurriculumTypes");
            DropTable("dbo.CurriculumWeeks");
            DropTable("dbo.CurriculumDetails");
            DropTable("dbo.CurriculumBlocks");
            DropTable("dbo.Curricula");
            DropTable("dbo.Teams");
            DropTable("dbo.Coaches");
            DropTable("dbo.Clubs");
            DropTable("dbo.ClubAdmins");
            DropTable("dbo.Chairmen");
            DropTable("dbo.Certificates");
            DropTable("dbo.BodyParts");
        }
    }
}
