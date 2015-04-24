﻿using System.Data.Entity;
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
        public virtual DbSet<CurriculumBlock> CurriculumBlocks { get; set; }
        public virtual DbSet<CurriculumDetail> CurriculumDetails { get; set; }
        public virtual DbSet<Curriculum> Curriculums { get; set; }
        public virtual DbSet<CurriculumType> CurriculumTypes { get; set; }
        public virtual DbSet<CurriculumWeek> CurriculumWeeks { get; set; }
        public virtual DbSet<FACourse> FACourses { get; set; }
        public virtual DbSet<HeadOfAcademy> HeadOfAcademies { get; set; }
        public virtual DbSet<HeadOfEducation> HeadOfEducations { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<NutritionAlternative> NutritionAlternatives { get; set; }
        public virtual DbSet<NutritionFoodType> NutritionFoodTypes { get; set; }
        public virtual DbSet<Physiotherapist> Physiotherapists { get; set; }
        public virtual DbSet<PhysiotherapyExercise> PhysiotherapyExercises { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerAttribute> PlayerAttributes { get; set; }
        public virtual DbSet<PlayerInjury> PlayerInjuries { get; set; }
        public virtual DbSet<Scout> Scouts { get; set; }
        public virtual DbSet<SportScientist> SportScientists { get; set; }
        public virtual DbSet<SportsScienceExercise> SportsScienceExercises { get; set; }
        public virtual DbSet<SportsScienceTest> SportsScienceTests { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<WelfareOfficer> WelfareOfficers { get; set; }
        public virtual DbSet<SkillLevel> SkillLevels { get; set; }
        public virtual DbSet<SkillVideo> SkillsVideos { get; set; }
        public virtual DbSet<ActivityStatusChange> ActivityStatusChanges { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
