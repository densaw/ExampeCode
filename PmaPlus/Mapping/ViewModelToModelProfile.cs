using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.CurriculumProcess;
using PmaPlus.Model.ViewModels.Diary;
using PmaPlus.Model.ViewModels.News;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Model.ViewModels.PlayerAttribute;
using PmaPlus.Model.ViewModels.Qualification;
using PmaPlus.Model.ViewModels.SiteSettings;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.SportsScience;
using PmaPlus.Model.ViewModels.TalentIdentifications;
using PmaPlus.Model.ViewModels.ToDo;
using PmaPlus.Model.ViewModels.TrainingTeamMember;

namespace PmaPlus.Mapping
{
    class ViewModelToModelProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToModelProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<SkillLevelViewModel, SkillLevel>();
            Mapper.CreateMap<SkillVideoViewModel, SkillVideo>();

            Mapper.CreateMap<PhysioBodyPartViewModel, BodyPart>();

            Mapper.CreateMap<PhysioBodyPartViewModel, PhysiotherapyExercise>();

            Mapper.CreateMap<PhysiotherapyExerciseViewModel, PhysiotherapyExercise>();

            Mapper.CreateMap<NutritionFoodTypeViewModel, NutritionFoodType>()
                .ForMember(d =>d.When,o=>o.Ignore());

            Mapper.CreateMap<NutritionAlternativeViewModel,NutritionAlternative>();

            Mapper.CreateMap<NutritionRecipeViewModel, NutritionRecipe>();

            Mapper.CreateMap<SportsScienceTestViewModel, SportsScienceTest>();

            Mapper.CreateMap<SportsScienceExerciseViewModel, SportsScienceExercise>();

            Mapper.CreateMap<TargetViewModel, TargetHistory>();

            Mapper.CreateMap<PasswordViewModel, PasswordHistory>();

            Mapper.CreateMap<ScenarioViewModel,Scenario>();

            Mapper.CreateMap<NutritionNewViewModel,NutritionNew >();

            Mapper.CreateMap<ExerciseNewViewModel,ExcerciseNew >();


            //ClubAdmin

            Mapper.CreateMap<SessionViewModel, Session>()
                .ForMember(d => d.Scenarios, o => o.Ignore());

            Mapper.CreateMap<PlayerAttributeViewModel,PlayerAttribute>();

            Mapper.CreateMap<ToDoViewModel, ToDo>();

            Mapper.CreateMap<AddDiaryViewModel, Diary>();

            Mapper.CreateMap<QualificationViewModel, Qualification>();

            Mapper.CreateMap<CurriculumViewModel, Curriculum>();


            Mapper.CreateMap<CurriculumStatementViewModel, CurriculumStatement>()
                .ForMember(d => d.Roles, o => o.Ignore());



            //Talent Identification

            Mapper.CreateMap<TalentIdentificationViewModel,TalentIdentification>();

            Mapper.CreateMap< AttributesOfTalentViewModel,AttributesOfTalent>();

            Mapper.CreateMap<TalentNoteViewModel,TalentNote>();


            Mapper.CreateMap<SessionAttendanceTableViewModel, SessionAttendance>();

            Mapper.CreateMap<PlayerObjectiveTableViewModel, PlayerObjective>();

            Mapper.CreateMap<PlayerBlockObjectiveTableViewModel, BlockObjectiveStatement>();

            Mapper.CreateMap<PlayerRatingsTableViewModel, PlayerRatings>();

        }
    }
}
