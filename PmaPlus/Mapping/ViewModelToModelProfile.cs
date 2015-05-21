using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.Diary;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Model.ViewModels.PlayerAttribute;
using PmaPlus.Model.ViewModels.Qualification;
using PmaPlus.Model.ViewModels.SiteSettings;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.SportsScience;
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
            Mapper.CreateMap<CurriculumTypeViewModel, CurriculumType>();
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

            //ClubAdmin

            Mapper.CreateMap<PlayerAttributeViewModel,PlayerAttribute>();

            Mapper.CreateMap<ToDoViewModel, ToDo>();

            Mapper.CreateMap<AddDiaryViewModel, Diary>();

            Mapper.CreateMap<QualificationViewModel, Qualification>();

            Mapper.CreateMap<CurriculumViewModel, Curriculum>();

            Mapper.CreateMap<CurriculumDetailViewModel, CurriculumDetail>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CurriculumDetailId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.CurriculumDetailName))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.CurriculumDetailNumber))
                .ForMember(d => d.PlayersDescription, o => o.MapFrom(s => s.CurriculumDetailPlayersDescription))
                .ForMember(d => d.PlayersFriendlyName, o => o.MapFrom(s => s.CurriculumDetailPlayersFriendlyName))
                .ForMember(d => d.PlayersFriendlyPicture, o => o.MapFrom(s => s.CurriculumDetailPlayersFriendlyPicture))
                .ForMember(d => d.CoachDescription, o => o.MapFrom(s => s.CurriculumDetailCoachDescription))
                .ForMember(d => d.CoachPicture, o => o.MapFrom(s => s.CurriculumDetailPlayersFriendlyPicture));

            Mapper.CreateMap<CurriculumStatementViewModel, CurriculumStatement>();
        }
    }
}
