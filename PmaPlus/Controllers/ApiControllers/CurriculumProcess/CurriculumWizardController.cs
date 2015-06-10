using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.CurriculumProcess;
using PmaPlus.Services;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.CurriculumProcess
{
    public class CurriculumWizardController : ApiController
    {
        private readonly CurriculumProcessServices _curriculumProcessServices;
        private readonly UserServices _userServices;
        private readonly TeamServices _teamServices;

        public CurriculumWizardController(CurriculumProcessServices curriculumProcessServices, UserServices userServices, TeamServices teamServices)
        {
            _curriculumProcessServices = curriculumProcessServices;
            _userServices = userServices;
            _teamServices = teamServices;
        }


        public IEnumerable<SessionsWizardViewModel> GetWizard(int id)
        {
            //var coach = _userServices.GetCoachByUserName(User.Identity.Name);

            //if (coach == null)
            //    return null;

            var sessions = _curriculumProcessServices.GetTeamSessions(id);
            var sesResults = _curriculumProcessServices.GetTeamSessionsResult(id);

            var team = _teamServices.GetTeamById(id).TeamCurriculum.Id;


            var result = from s in sessions

                // These two lines are the left join. 
                join sr in sesResults on s.Id equals sr.SessionId  into leftm
                from m in leftm.DefaultIfEmpty()

               //let description = m == null ? 0 : m.SessionId

                select new SessionsWizardViewModel()
                {
                    Attendance = s.Attendance,
                    Name = s.Name,
                    Rating = s.Rating,
                    Number = s.Number,
                    NeedScenarios = s.NeedScenarios,
                    EndOfReviewPeriod = s.EndOfReviewPeriod,
                    Done = m.Done,
                    ObjectiveReport = s.ObjectiveReport,
                    Objectives = s.Objectives,
                    Report = s.Report,
                    CoachDetails = s.CoachDetails,
                    CoachDetailsName = s.CoachDetailsName,
                    CoachPicture = s.CoachPicture,
                    PlayerDetails = s.PlayerDetails,
                    PlayerDetailsName = s.PlayerDetailsName,
                    PlayerPicture = s.PlayerPicture,
                    StartOfReviewPeriod = s.StartOfReviewPeriod,
                    StartedOn = m.StartedOn,
                    ComletedOn = m.ComletedOn,
                    SessionId = s.Id,
                    TeamCurriculumId = team
                };



            //var list = sessions.Join(sesResults.DefaultIfEmpty(), s => s.Id, sr => sr.SessionId, (s, sr) => new {s, sr})
            //    .Select(i => new SessionsWizardViewModel()
            //    {
            //        Attendance = i.s.Attendance,
            //        Name = i.s.Name,
            //        Rating = i.s.Rating,
            //        Number = i.s.Number,
            //        NeedScenarios = i.s.NeedScenarios,
            //        EndOfReviewPeriod = i.s.EndOfReviewPeriod,
            //        Done = i.sr.Done,
            //        ObjectiveReport = i.s.ObjectiveReport,
            //        Objectives = i.s.Objectives,
            //        Report = i.s.Report,
            //        CoachDetails = i.s.CoachDetails,
            //        CoachDetailsName = i.s.CoachDetailsName,
            //        CoachPicture = i.s.CoachPicture,
            //        PlayerDetails = i.s.PlayerDetails,
            //        PlayerDetailsName = i.s.PlayerDetailsName,
            //        PlayerPicture = i.s.PlayerPicture,
            //        StartOfReviewPeriod = i.s.StartOfReviewPeriod,
            //        StartedOn = i.sr.StartedOn,
            //        ComletedOn = i.sr.ComletedOn,
            //        SessionId = i.s.Id,
            //        TeamCurriculumId = team
            //    });


            return result.ToList();





        }





    }
}
