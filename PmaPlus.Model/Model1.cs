//namespace PmaPlus.Model
//{
    
//    using System.Data.Entity;
    

//    public  class Model1 : DbContext
//    {
//        public Model1()
//            : base()
//        {
//        }

//        public virtual DbSet<Address> Addresses { get; set; }
//        public virtual DbSet<BodyPart> BodyParts { get; set; }
//        public virtual DbSet<Certificate> Certificates { get; set; }
//        public virtual DbSet<Chairman> Chairmen { get; set; }
//        public virtual DbSet<Club> Clubs { get; set; }
//        public virtual DbSet<ClubAdmin> ClubAdmins { get; set; }
//        public virtual DbSet<Coach> Coaches { get; set; }
//        public virtual DbSet<CurriculumBlock> CurriculumBlocks { get; set; }
//        public virtual DbSet<CurriculumDetail> CurriculumDetails { get; set; }
//        public virtual DbSet<Curriculum> Curriculums { get; set; }
//        public virtual DbSet<CurriculumType> CurriculumTypes { get; set; }
//        public virtual DbSet<CurriculumWeek> CurriculumWeeks { get; set; }
//        public virtual DbSet<FACourse> FACourses { get; set; }
//        public virtual DbSet<HeadOfAcademy> HeadOfAcademies { get; set; }
//        public virtual DbSet<HeadOfEducation> HeadOfEducations { get; set; }
//        public virtual DbSet<Match> Matches { get; set; }
//        public virtual DbSet<NutritionAlternative> NutritionAlternatives { get; set; }
//        public virtual DbSet<NutritionFoodType> NutritionFoodTypes { get; set; }
//        public virtual DbSet<Physiotherapist> Physiotherapists { get; set; }
//        public virtual DbSet<PhysiotherapyExercise> PhysiotherapyExercises { get; set; }
//        public virtual DbSet<Player> Players { get; set; }
//        public virtual DbSet<PlayerAttribute> PlayerAttributes { get; set; }
//        public virtual DbSet<PlayerInjury> PlayerInjuries { get; set; }
//        public virtual DbSet<Scout> Scouts { get; set; }
//        public virtual DbSet<SportScientist> SportScientists { get; set; }
//        public virtual DbSet<SportsScienceExercise> SportsScienceExercises { get; set; }
//        public virtual DbSet<SportsScienceTest> SportsScienceTests { get; set; }
//        public virtual DbSet<Team> Teams { get; set; }
//        public virtual DbSet<User> Users { get; set; }
//        public virtual DbSet<UserDetail> UserDetails { get; set; }
//        public virtual DbSet<WelfareOfficer> WelfareOfficers { get; set; }
//        public virtual DbSet<Skill> Skills { get; set; }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Address>()
//                .Property(e => e.Address1)
//                .IsUnicode(false);

//            modelBuilder.Entity<Address>()
//                .Property(e => e.Address2)
//                .IsUnicode(false);

//            modelBuilder.Entity<Address>()
//                .Property(e => e.Address3)
//                .IsUnicode(false);

//            modelBuilder.Entity<Address>()
//                .Property(e => e.Telephone)
//                .IsUnicode(false);

//            modelBuilder.Entity<Address>()
//                .Property(e => e.Mobile)
//                .IsUnicode(false);

//            modelBuilder.Entity<Address>()
//                .Property(e => e.TownCity)
//                .IsUnicode(false);

//            modelBuilder.Entity<BodyPart>()
//                .Property(e => e.InjuryName)
//                .IsUnicode(false);

//            modelBuilder.Entity<BodyPart>()
//                .Property(e => e.Description)
//                .IsFixedLength();

//            modelBuilder.Entity<BodyPart>()
//                .Property(e => e.Picture)
//                .IsFixedLength();

//            modelBuilder.Entity<BodyPart>()
//                .Property(e => e.Treatment)
//                .IsFixedLength();

//            modelBuilder.Entity<Certificate>()
//                .Property(e => e.CertificateName)
//                .IsFixedLength();

//            modelBuilder.Entity<Chairman>()
//                .Property(e => e.Chairman1)
//                .IsUnicode(false);

//            modelBuilder.Entity<Chairman>()
//                .Property(e => e.Email)
//                .IsUnicode(false);

//            modelBuilder.Entity<Chairman>()
//                .Property(e => e.Telephone)
//                .IsUnicode(false);

//            modelBuilder.Entity<Club>()
//                .Property(e => e.ClubName)
//                .IsUnicode(false);

//            modelBuilder.Entity<Club>()
//                .Property(e => e.ClubLogo)
//                .IsUnicode(false);

//            modelBuilder.Entity<Club>()
//                .HasOptional(e => e.Chairman)
//                .WithRequired(e => e.Club);

//            modelBuilder.Entity<Club>()
//                .HasMany(e => e.Coaches)
//                .WithRequired(e => e.Club)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<Club>()
//                .HasMany(e => e.Teams)
//                .WithRequired(e => e.Club)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<CurriculumDetail>()
//                .Property(e => e.Name)
//                .IsFixedLength();

//            modelBuilder.Entity<CurriculumDetail>()
//                .Property(e => e.Number)
//                .IsFixedLength();

//            modelBuilder.Entity<CurriculumDetail>()
//                .Property(e => e.CoachDescription)
//                .IsFixedLength();

//            modelBuilder.Entity<CurriculumDetail>()
//                .Property(e => e.PlayersFriendlyName)
//                .IsFixedLength();

//            modelBuilder.Entity<CurriculumDetail>()
//                .Property(e => e.PlayersFriendlyPicture)
//                .IsFixedLength();

//            modelBuilder.Entity<CurriculumDetail>()
//                .Property(e => e.PlayersDescription)
//                .IsFixedLength();

//            modelBuilder.Entity<CurriculumDetail>()
//                .HasMany(e => e.CurriculumBlocks)
//                .WithOptional(e => e.CurriculumDetail)
//                .HasForeignKey(e => e.CurriculumDetailsId);

//            modelBuilder.Entity<CurriculumDetail>()
//                .HasMany(e => e.Curriculums)
//                .WithOptional(e => e.CurriculumDetail)
//                .HasForeignKey(e => e.CurriculumDetailsId);

//            modelBuilder.Entity<CurriculumDetail>()
//                .HasMany(e => e.CurriculumWeeks)
//                .WithOptional(e => e.CurriculumDetail)
//                .HasForeignKey(e => e.CurriculumDetailsId);

//            modelBuilder.Entity<Curriculum>()
//                .Property(e => e.Name)
//                .IsFixedLength();

//            modelBuilder.Entity<FACourse>()
//                .Property(e => e.CourseName)
//                .IsUnicode(false);

//            modelBuilder.Entity<FACourse>()
//                .Property(e => e.CourseDescriprion)
//                .IsUnicode(false);

//            modelBuilder.Entity<Match>()
//                .Property(e => e.OppositionTeam)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionAlternative>()
//                .Property(e => e.VideoLink)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.FoodName)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.GoodFor)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.PortionSize)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Description)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Picture)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Calories)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.CaloriesFromFat)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.TotalFat)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.SaturatedFat)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.TransFat)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Cholesterol)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Sodium)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Potassium)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.TotalCrbohydrate)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.DietaryFibre)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Sugars)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Protein)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.VitaminA)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.VitaminB)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.VitaminC)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.VitaminD)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.VitaminE)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Iron)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.Calcium)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .Property(e => e.DailyIntake)
//                .IsFixedLength();

//            modelBuilder.Entity<NutritionFoodType>()
//                .HasMany(e => e.NutritionAlternatives)
//                .WithOptional(e => e.NutritionFoodType)
//                .HasForeignKey(e => e.BadItemId);

//            modelBuilder.Entity<NutritionFoodType>()
//                .HasMany(e => e.NutritionAlternatives1)
//                .WithOptional(e => e.NutritionFoodType1)
//                .HasForeignKey(e => e.AlternativeId);

//            modelBuilder.Entity<Physiotherapist>()
//                .HasMany(e => e.Clubs)
//                .WithRequired(e => e.Physiotherapist)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<PhysiotherapyExercise>()
//                .Property(e => e.Type)
//                .IsFixedLength();

//            modelBuilder.Entity<PhysiotherapyExercise>()
//                .Property(e => e.Name)
//                .IsFixedLength();

//            modelBuilder.Entity<PhysiotherapyExercise>()
//                .Property(e => e.Description)
//                .IsFixedLength();

//            modelBuilder.Entity<PhysiotherapyExercise>()
//                .Property(e => e.Videolink)
//                .IsFixedLength();

//            modelBuilder.Entity<PhysiotherapyExercise>()
//                .Property(e => e.Picture)
//                .IsFixedLength();

//            modelBuilder.Entity<PlayerAttribute>()
//                .Property(e => e.AttributeName)
//                .IsFixedLength();

//            modelBuilder.Entity<PlayerInjury>()
//                .Property(e => e.InjuryName)
//                .IsFixedLength();

//            modelBuilder.Entity<PlayerInjury>()
//                .Property(e => e.Stage)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.TestName)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.TestDescription)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.ZScoreFormula)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.Measure)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.LowValue)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.HightValue)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.NationalAverage)
//                .IsFixedLength();

//            modelBuilder.Entity<SportsScienceTest>()
//                .Property(e => e.TestVideo)
//                .IsFixedLength();

//            modelBuilder.Entity<Team>()
//                .Property(e => e.Name)
//                .IsFixedLength();

//            modelBuilder.Entity<Team>()
//                .HasMany(e => e.Players)
//                .WithOptional(e => e.Team)
//                .HasForeignKey(e => e.Team1Id);

//            modelBuilder.Entity<Team>()
//                .HasMany(e => e.Players1)
//                .WithOptional(e => e.Team1)
//                .HasForeignKey(e => e.Team2Id);

//            modelBuilder.Entity<User>()
//                .Property(e => e.Email)
//                .IsUnicode(false);

//            modelBuilder.Entity<User>()
//                .Property(e => e.Password)
//                .IsUnicode(false);

//            modelBuilder.Entity<User>()
//                .HasMany(e => e.SportScientists)
//                .WithRequired(e => e.User)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<UserDetail>()
//                .Property(e => e.FirstName)
//                .IsUnicode(false);

//            modelBuilder.Entity<UserDetail>()
//                .Property(e => e.LastName)
//                .IsUnicode(false);

//            modelBuilder.Entity<UserDetail>()
//                .Property(e => e.AboutMe)
//                .IsFixedLength();

//            modelBuilder.Entity<UserDetail>()
//                .Property(e => e.ProfilePocture)
//                .IsFixedLength();

//            modelBuilder.Entity<UserDetail>()
//                .Property(e => e.Nationality)
//                .IsFixedLength();

//            modelBuilder.Entity<UserDetail>()
//                .HasMany(e => e.Users)
//                .WithOptional(e => e.UserDetail)
//                .HasForeignKey(e => e.DetailsId);

//            modelBuilder.Entity<Skill>()
//                .Property(e => e.SkillName)
//                .IsUnicode(false);

//            modelBuilder.Entity<Skill>()
//                .Property(e => e.VideoLink)
//                .IsUnicode(false);

//            modelBuilder.Entity<Skill>()
//                .Property(e => e.Description)
//                .IsUnicode(false);
//        }
//    }
//}
