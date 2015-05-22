using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PmaPlus.Model;
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
using PmaPlus.Model.ViewModels.Team;
using PmaPlus.Model.ViewModels.ToDo;
using PmaPlus.Model.ViewModels.TrainingTeamMember;

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
            Mapper.CreateMap<User, UsersList>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.UserDetail.FirstName + " " + s.UserDetail.LastName));

            Mapper.CreateMap<CurriculumType, CurriculumTypeViewModel>();
            Mapper.CreateMap<CurriculumType, CurriculumTypesList>();

            Mapper.CreateMap<SkillLevel, SkillLevelViewModel>();
            Mapper.CreateMap<SkillLevel, SkillLevelTableViewModel>();

            Mapper.CreateMap<SkillVideo, SkillVideoViewModel>();
            Mapper.CreateMap<SkillVideo, SkillVideoTableViewModel>()
                .ForMember(dest => dest.TrainingItemName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Duration));

            Mapper.CreateMap<BodyPart, PhysioBodyPartTableViewModel>();
            Mapper.CreateMap<BodyPart, PhysioBodyPartViewModel>();

            Mapper.CreateMap<PhysiotherapyExercise, PhysiotherapyExerciseTableViewModel>();
            Mapper.CreateMap<PhysiotherapyExercise, PhysiotherapyExerciseViewModel>();

            Mapper.CreateMap<NutritionFoodType, NutritionFoodTypeViewModel>()
                .ForMember(d => d.Whens, o => o.MapFrom(s=> s.When.Select(t=>t.Type)));
            Mapper.CreateMap<NutritionFoodType, NutritionFoodTypeTableViewModel>()
                .ForMember(d => d.When , o => o.MapFrom(s => s.When.Select(t=>t.Type)));

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

            Mapper.CreateMap<PasswordHistory, PasswordHistoryTableViewModel>();

            Mapper.CreateMap<Scenario, ScenarioViewModel>();
            Mapper.CreateMap<Scenario, ScenarioTableViewModel>();

            //club admin


            Mapper.CreateMap<PlayerAttribute, PlayerAttributeTableViewModel>();
            Mapper.CreateMap<PlayerAttribute, PlayerAttributeViewModel>();

            Mapper.CreateMap<User, TrainingTeamMemberPlateViewModel>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.UserDetail.FirstName + s.UserDetail.LastName))
                .ForMember(d => d.TownCity, o => o.MapFrom(s => s.UserDetail.Address.TownCity))
                .ForMember(d => d.BirthDay, o => o.MapFrom(s => s.UserDetail.Birthday))
                .ForMember(d => d.Age, o => o.MapFrom(s => DateTime.Now.Year - s.UserDetail.Birthday.Value.Year))
                .ForMember(d => d.Mobile, o => o.MapFrom(s => s.UserDetail.Address.Mobile))
                .ForMember(d => d.CrbDbsExpiry, o => o.MapFrom(s => s.UserDetail.CrbDbsExpiry))
                .ForMember(d => d.FirstAidExpiry, o => o.MapFrom(s => s.UserDetail.FirstAidExpiry))
                .ForMember(d => d.LastLogin, o => o.MapFrom(s => s.LoggedAt))
                .ForMember(d => d.ProfilePicture, o => o.MapFrom(s => s.UserDetail.ProfilePicture));

            Mapper.CreateMap<ToDo, ToDoViewModel>();

            Mapper.CreateMap<Diary, DiaryViewModel>();

            Mapper.CreateMap<Qualification, QualificationViewModel>();

            Mapper.CreateMap<Curriculum, CurriculumViewModel>();

            Mapper.CreateMap<CurriculumDetail, CurriculumDetailViewModel>();
            Mapper.CreateMap<Curriculum, CurriculumDetailViewModel>();
            Mapper.CreateMap<CurriculumBlock, CurriculumDetailViewModel>();
            Mapper.CreateMap<CurriculumWeek, CurriculumDetailViewModel>();
            Mapper.CreateMap<CurriculumSession, CurriculumDetailViewModel>();


            Mapper.CreateMap<CurriculumStatement, CurriculumStatementViewModel>()
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles.Select(r => r.Role)));

            Mapper.CreateMap<Team, TeamTableViewModel>()
                .ForMember(d => d.CurriculumName, o => o.MapFrom(s => s.TeamCurriculum.Curriculum.Name))
                .ForMember(d=>d.Progress, o=>  o.MapFrom(s =>s.TeamCurriculum.Progress));

            Mapper.CreateMap<Team, AddTeamViewModel>()
                .ForMember(d => d.Coaches, o => o.MapFrom(s => s.Coaches.Select(c => c.Id)))
                .ForMember(d => d.Players, o => o.MapFrom(s => s.Players.Select(p => p.Id)))
                .ForMember(d => d.CurriculumId, o => o.MapFrom(s => s.TeamCurriculum.Curriculum.Id));
            Mapper.CreateMap<Team, TeamsList>();
        }
    }
}
