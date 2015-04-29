using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Model.ViewModels.SiteSettings;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.SportsScience;

namespace PmaPlus.Mapping
{
    class ModelToViewModelProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ModelToViewModelProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<CurriculumType, CurriculumTypeViewModel>();

            Mapper.CreateMap<SkillLevel, SkillLevelViewModel>();

            Mapper.CreateMap<SkillVideo, SkillVideoViewModel>();
            Mapper.CreateMap<SkillVideo, SkillVideoTableViewModel>()
                .ForMember(dest => dest.TrainingItemName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Duration));

            Mapper.CreateMap<BodyPart, PhysioBodyPartTableViewModel>();
            Mapper.CreateMap<BodyPart, PhysioBodyPartViewModel>();

            Mapper.CreateMap<PhysiotherapyExercise, PhysiotherapyExerciseTableViewModel>();
            Mapper.CreateMap<PhysiotherapyExercise, PhysiotherapyExerciseViewModel>();

            Mapper.CreateMap<NutritionFoodType, NutritionFoodTypeViewModel>();
            Mapper.CreateMap<NutritionFoodType, NutritionFoodTypeTableViewModel>();

            Mapper.CreateMap<NutritionAlternative, NutritionAlternativeViewModel>();
            Mapper.CreateMap<NutritionAlternative, NutritionAlternativeTableViewModel>();

            Mapper.CreateMap<NutritionRecipe, NutritionRecipeViewModel>();
            Mapper.CreateMap<NutritionRecipe, NutritionRecipeTableViewModel>();

            Mapper.CreateMap<SportsScienceTest, SportsScienceTestViewModel>();
            Mapper.CreateMap<SportsScienceTest, SportsScienceTestTableViewModel>();

            Mapper.CreateMap<SportsScienceExercise, SportsScienceExerciseViewModel>();
            Mapper.CreateMap<SportsScienceExercise, SportsScienceExerciseTableViewModel>();

            Mapper.CreateMap<TargetHistory, TargetHistoryTableViewModel>();
            Mapper.CreateMap<TargetHistory, TargetViewModel>();
        }
    }
}
