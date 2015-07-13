using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PmaPlus.Model;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.CurriculumProcess;
using PmaPlus.Model.ViewModels.Diary;
using PmaPlus.Model.ViewModels.Matches;
using PmaPlus.Model.ViewModels.News;
using PmaPlus.Model.ViewModels.Nutrition;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Model.ViewModels.PlayerAttribute;
using PmaPlus.Model.ViewModels.Qualification;
using PmaPlus.Model.ViewModels.Scenario;
using PmaPlus.Model.ViewModels.SiteSettings;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.SportsScience;
using PmaPlus.Model.ViewModels.TalentIdentifications;
using PmaPlus.Model.ViewModels.Team;
using PmaPlus.Model.ViewModels.ToDo;
using PmaPlus.Model.ViewModels.TrainingTeamMember;
using PmaPlus.Services.Services;

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
                .ForMember(d => d.Whens, o => o.MapFrom(s => s.When.Select(t => t.Type)));
            Mapper.CreateMap<NutritionFoodType, NutritionFoodTypeTableViewModel>()
                .ForMember(d => d.When, o => o.MapFrom(s => s.When.Select(t => t.Type)));

            Mapper.CreateMap<NutritionAlternative, NutritionAlternativeViewModel>();
            Mapper.CreateMap<NutritionAlternative, NutritionAlternativeTableViewModel>();

            Mapper.CreateMap<NutritionRecipe, NutritionRecipeViewModel>();
            Mapper.CreateMap<NutritionRecipe, NutritionRecipeTableViewModel>();

            Mapper.CreateMap<NutritionNew, NutritionNewViewModel>();
            Mapper.CreateMap<NutritionNew, NutritionNewTableViewModel>();

            Mapper.CreateMap<ExcerciseNew, ExerciseNewViewModel>();
            Mapper.CreateMap<ExcerciseNew, ExerciseNewTableViewModel>();


            Mapper.CreateMap<SportsScienceTest, SportsScienceTestViewModel>();
            Mapper.CreateMap<SportsScienceTest, SportsScienceTestTableViewModel>();

            Mapper.CreateMap<SportsScienceExercise, SportsScienceExerciseViewModel>();
            Mapper.CreateMap<SportsScienceExercise, SportsScienceExerciseTableViewModel>();

            Mapper.CreateMap<TargetHistory, TargetHistoryTableViewModel>();
            Mapper.CreateMap<TargetHistory, TargetViewModel>();

            Mapper.CreateMap<PasswordHistory, PasswordHistoryTableViewModel>();

            Mapper.CreateMap<Scenario, ScenarioViewModel>();
            Mapper.CreateMap<Scenario, ScenarioTableViewModel>();
            Mapper.CreateMap<Scenario, ScenarioList>();

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

            Mapper.CreateMap<Scout, UsersList>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.User.UserDetail.FirstName + " " + s.User.UserDetail.LastName));


            Mapper.CreateMap<ToDo, ToDoViewModel>();

            Mapper.CreateMap<Diary, DiaryViewModel>();

            Mapper.CreateMap<Qualification, QualificationViewModel>();

            Mapper.CreateMap<Curriculum, CurriculumViewModel>();
            Mapper.CreateMap<Curriculum, CurriculumTableViewModel>()
                .ForMember(d => d.Started, o => o.MapFrom(d => d.TeamCurricula.FirstOrDefault() != null ? d.TeamCurricula.FirstOrDefault().StartedOn.HasValue : false));

            Mapper.CreateMap<Session, SessionViewModel>()
                .ForMember(d => d.Scenarios, o => o.MapFrom(s => s.Scenarios.Select(sc => sc.Id)));
            Mapper.CreateMap<Session, SessionTableViewModel>();



            Mapper.CreateMap<CurriculumStatement, CurriculumStatementViewModel>()
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles.Select(r => r.Role)));

            Mapper.CreateMap<Team, TeamTableViewModel>()
                .ForMember(d => d.CurriculumName, o => o.MapFrom(s => s.TeamCurriculum.Curriculum.Name))
                .ForMember(d => d.Progress,
                    o =>
                        o.MapFrom(
                            s =>
                                ((decimal) s.TeamCurriculum.SessionResults.Count(sr => sr.Done)/
                                 (s.TeamCurriculum.SessionResults.Count == 0 ? 1 : s.TeamCurriculum.SessionResults.Count))*
                                100))
                .ForMember(d => d.Archived, o => o.MapFrom(s => s.TeamCurriculum.Archived));

            Mapper.CreateMap<Team, AddTeamViewModel>()
                .ForMember(d => d.Coaches, o => o.MapFrom(s => s.Coaches.Select(c => c.User.Id)))
                .ForMember(d => d.Players, o => o.MapFrom(s => s.Players.Select(p => p.User.Id)))
                .ForMember(d => d.Archived, o => o.MapFrom(s => s.TeamCurriculum.Archived))
                .ForMember(d => d.CurriculumId, o => o.MapFrom(s => s.TeamCurriculum.Curriculum.Id));
            Mapper.CreateMap<Team, TeamsList>();



            //TalenIdentification

            Mapper.CreateMap<TalentIdentification, TalentIdentificationViewModel>();
            Mapper.CreateMap<TalentIdentification, TalentIdentificationTableViewModel>()
                .ForMember(d => d.Age, o => o.MapFrom(s => DateTime.Now.Year - s.BirthDate.Year))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Score, o => o.MapFrom(s => ((decimal)s.Attributes.Sum(a => a.Score) / (s.Attributes.Sum(a => a.Attribute.MaxScore) == 0 ? 1 : s.Attributes.Sum(a => a.Attribute.MaxScore))) * 100))
                .ForMember(d => d.ScouteName, o => o.MapFrom(s => s.Scout.User.UserDetail.FirstName + " " + s.Scout.User.UserDetail.LastName));

            Mapper.CreateMap<TalentIdentification, TalentIdentificationDetailViewModel>()
                .ForMember(d => d.AttributeScorePers, o => o.MapFrom(s => (s.Attributes.Sum(a => a.Score) / (s.Attributes.Sum(a => a.Attribute.MaxScore) == 0 ? 1 : s.Attributes.Sum(a => a.Attribute.MaxScore))) * 100));

            Mapper.CreateMap<AttributesOfTalent, AttributesOfTalentViewModel>()
                .ForMember(d => d.AttributeName, o => o.MapFrom(s => s.Attribute.Name));

            Mapper.CreateMap<TalentNote, TalentNoteViewModel>();


            Mapper.CreateMap<Session, SessionsWizardViewModel>();

            #region Curriculum Process

            Mapper.CreateMap<SessionResult, SessionsWizardViewModel>()
                .ForMember(d => d.Attendance, o => o.MapFrom(s => s.Session.Attendance))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Session.Name))
                .ForMember(d => d.Rating, o => o.MapFrom(s => s.Session.Rating))
                .ForMember(d => d.Number, o => o.MapFrom(s => s.Session.Number))
                .ForMember(d => d.NeedScenarios, o => o.MapFrom(s => s.Session.NeedScenarios))
                .ForMember(d => d.EndOfReviewPeriod, o => o.MapFrom(s => s.Session.EndOfReviewPeriod))
                .ForMember(d => d.SessionId, o => o.MapFrom(s => s.Session.Id))
                .ForMember(d => d.ObjectiveReport, o => o.MapFrom(s => s.Session.ObjectiveReport))
                .ForMember(d => d.Objectives, o => o.MapFrom(s => s.Session.Objectives))
                .ForMember(d => d.Report, o => o.MapFrom(s => s.Session.Report))
                .ForMember(d => d.CoachDetails, o => o.MapFrom(s => s.Session.CoachDetails))
                .ForMember(d => d.CoachDetailsName, o => o.MapFrom(s => s.Session.CoachDetailsName))
                .ForMember(d => d.CoachPicture,o => o.MapFrom(s => "/api/file/Sessions/" + s.Session.CoachPicture + "/" + s.Session.Id))
                .ForMember(d => d.PlayerDetails, o => o.MapFrom(s => s.Session.PlayerDetails))
                .ForMember(d => d.PlayerDetailsName, o => o.MapFrom(s => s.Session.PlayerDetailsName))
                .ForMember(d => d.PlayerPicture,o => o.MapFrom(s => "/api/file/Sessions/" + s.Session.PlayerPicture + "/" + s.Session.Id))
                .ForMember(d => d.StartOfReviewPeriod, o => o.MapFrom(s => s.Session.StartOfReviewPeriod))
                .ForMember(d => d.TeamCurriculumId, o => o.MapFrom(s => s.TeamCurriculumId));

            Mapper.CreateMap<PlayerBlockObjective, AddPlayerBlockObjectiveTableViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.PlayerId, o => o.MapFrom(s => s.PlayerId))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Player.User.UserDetail.FirstName + " " + s.Player.User.UserDetail.LastName))
                .ForMember(d => d.Picture,o => o.MapFrom( s => "/api/file/ProfilePicture/" + s.Player.User.UserDetail.ProfilePicture + "/" + s.Player.User.Id))
                .ForMember(d => d.PreObjective, o => o.MapFrom(s => s.PreObjective));


            Mapper.CreateMap<PlayerObjective, AddPlayerObjectiveTableViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.PlayerId, o => o.MapFrom(s => s.PlayerId))
                .ForMember(d => d.Picture,o => o.MapFrom(s => "/api/file/ProfilePicture/" + s.Player.User.UserDetail.ProfilePicture + "/" +s.Player.User.Id))
                .ForMember(d => d.Name,o => o.MapFrom(s => s.Player.User.UserDetail.FirstName + " " + s.Player.User.UserDetail.LastName))
                .ForMember(d => d.Objective, o => o.MapFrom(s => s.Objective));

            Mapper.CreateMap<PlayerObjective, PlayerObjectiveTableViewModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.PlayerId, o => o.MapFrom(s => s.PlayerId))
                .ForMember(d => d.Picture,o =>o.MapFrom(s =>"/api/file/ProfilePicture/" + s.Player.User.UserDetail.ProfilePicture + "/" +s.Player.User.Id))
                .ForMember(d => d.Name,o => o.MapFrom(s => s.Player.User.UserDetail.FirstName + " " + s.Player.User.UserDetail.LastName))
                .ForMember(d => d.Objective, o => o.MapFrom(s => s.Objective))
                .ForMember(d => d.Outcome, o => o.MapFrom(s => s.Outcome))
                .ForMember(d => d.FeedBack, o => o.MapFrom(s => s.FeedBack));

            #endregion



            #region Match Reports

            Mapper.CreateMap<Match, MatchReportViewModel>()
                .ForMember(d => d.Mom,o => o.MapFrom(s => s.MatchMom.Player.User.UserDetail.FirstName + " " + s.MatchMom.Player.User.UserDetail.LastName));

            Mapper.CreateMap<Match, MatchReportTableViewModel>()
                .ForMember(d => d.Won, o => o.MapFrom(s => s.GoalsFor > s.GoalsAway))
                .ForMember(d => d.Mom, o => o.MapFrom(s => s.MatchMom.Player.User.UserDetail.FirstName + " " + s.MatchMom.Player.User.UserDetail.LastName));
            #endregion

        }
    }
}
