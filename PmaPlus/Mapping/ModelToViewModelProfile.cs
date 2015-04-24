using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.Skill;

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


        }
    }
}
