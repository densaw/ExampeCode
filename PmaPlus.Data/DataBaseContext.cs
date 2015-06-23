using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Reflection.Emit;
using PmaPlus.Data.Migrations;
using PmaPlus.Model;
using PmaPlus.Model.Models;


namespace PmaPlus.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("DefaultConnection")
        {
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<BodyPart> BodyParts { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Chairman> Chairmen { get; set; }
        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<ClubAdmin> ClubAdmins { get; set; }
        public virtual DbSet<Coach> Coaches { get; set; }
        public virtual DbSet<SessionResult> SessionResults { get; set; }
        public virtual DbSet<Curriculum> Curriculums { get; set; }
        public virtual DbSet<TeamCurriculum> TeamCurriculum { get; set; }
        public virtual DbSet<Session> CurriculumSessions { get; set; }
        public virtual DbSet<SessionAttendance> SessionAttendances { get; set; }

        public virtual DbSet<PlayerObjective> PlayerObjectives { get; set; }
        public virtual DbSet<PlayerBlockObjective> PlayerBlockObjectives { get; set; }
        public virtual DbSet<BlockObjectiveStatement> BlockObjectiveStatements { get; set; }
        public virtual DbSet<PlayerRatings> PlayerRatingses { get; set; }


        public virtual DbSet<FACourse> FACourses { get; set; }
        public virtual DbSet<HeadOfAcademy> HeadOfAcademies { get; set; }
        public virtual DbSet<HeadOfEducation> HeadOfEducations { get; set; }

        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<PlayerMatchObjective> MatchObjectives { get; set; }
        public virtual DbSet<PlayerMatchStatistic> PlayerMatchStatistics { get; set; }
        public virtual DbSet<MatchMom> MatchMoms { get; set; }


        public virtual DbSet<NutritionAlternative> NutritionAlternatives { get; set; }
        public virtual DbSet<NutritionFoodType> NutritionFoodTypes { get; set; }
        public virtual DbSet<FoodTypeToWhen> FoodTypeToWhens { get; set; }
        public virtual DbSet<NutritionRecipe> NutritionRecipes { get; set; }
        public virtual DbSet<Physiotherapist> Physiotherapists { get; set; }
        public virtual DbSet<PhysiotherapyExercise> PhysiotherapyExercises { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerAttribute> PlayerAttributes { get; set; }
        public virtual DbSet<PlayerInjury> PlayerInjuries { get; set; }
        public virtual DbSet<Scout> Scouts { get; set; }
        public virtual DbSet<SportScientist> SportScientists { get; set; }
        public virtual DbSet<SportsScienceExercise> SportsScienceExercises { get; set; }
        public virtual DbSet<SportsScienceTest> SportsScienceTests { get; set; }
        public virtual DbSet<Scenario> Scenarios { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<WelfareOfficer> WelfareOfficers { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }
        public virtual DbSet<SkillVideo> SkillsVideos { get; set; }
        public virtual DbSet<ActivityStatusChange> ActivityStatusChanges { get; set; }
        public virtual DbSet<PasswordHistory> PasswordHistory { get; set; }
        public virtual DbSet<TargetHistory> TargetHistories { get; set; }
        public virtual DbSet<ToDo> ToDos { get; set; }
        public virtual DbSet<Qualification> Qualifications { get; set; }
        public virtual DbSet<QualificationToFaCourse> QualificationToFaCourses { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<DiaryRecipient> DairyRecipients { get; set; }

        public virtual DbSet<TalentIdentification> TalentIdentifications { get; set; }
        public virtual DbSet<TalentNote> TalentNotes { get; set; }
        public virtual DbSet<AttributesOfTalent> AttributesOfTalents { get; set; }



        public virtual DbSet<CurriculumStatement> CurriculumStatements { get; set; }
        public virtual DbSet<StatementRoles> StatementRoles { get; set; }

        public virtual DbSet<ExcerciseNew> ExcerciseNews { get; set; }
        public virtual DbSet<NutritionNew> NutritionNews { get; set; }

        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageRating> MessageRatings { get; set; }
        public virtual DbSet<MessageComment> MessageComments { get; set; }


        #region Documents Sharing
        public virtual DbSet<SharedFolder> SharedFolders { get; set; }
        public virtual DbSet<SharedFolderRole> SharedFolderRoles { get; set; }
        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Configuration>());

            modelBuilder.Entity<Club>()
                .HasOptional(c => c.ClubAdmin)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Club>()
                .HasOptional(c => c.Address)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ClubAdmin>()
                .HasOptional(c => c.User)
                .WithOptionalDependent()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<User>()
                .HasOptional(u => u.UserDetail)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserDetail>()
                .HasOptional(u => u.Address)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessageComment>()
                .HasRequired(s => s.Message)
                .WithMany(s => s.Comments)
                .HasForeignKey(s => s.MessageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessageRating>()
                .HasRequired(s => s.Messages)
                .WithMany(s => s.Ratings)
                .HasForeignKey(s => s.MessagesId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessageComment>()
                .HasRequired(s => s.User)
                .WithMany(s => s.Comments)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessageRating>()
                .HasRequired(s => s.User)
                .WithMany(s => s.Ratings)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessagePrivate>()
                .HasRequired(s => s.MessageGroup)
                .WithMany(s => s.MessagePrivates)
                .HasForeignKey(s => s.MessageGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MessagePrivate>()
                .HasRequired(s => s.User)
                .WithMany(s => s.MessagePrivates)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(c => c.MessageGroups)
                .WithMany(c => c.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserRefId");
                    cs.MapRightKey("MessageGroupRefId");
                    cs.ToTable("UserToMessageGroup");
                });


            base.OnModelCreating(modelBuilder);
        }
    }
}
