using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.CurriculumProcess;

namespace PmaPlus.Services.Services
{
    public class CurriculumProcessServices
    {
        private readonly CurriculumServices _curriculumServices;
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamCurriculumRepository _teamCurriculumRepository;
        private readonly ISessionResultRepository _sessionResultRepository;

        public CurriculumProcessServices(CurriculumServices curriculumServices, ITeamRepository teamRepository, ISessionResultRepository sessionResultRepository, ITeamCurriculumRepository teamCurriculumRepository)
        {
            _curriculumServices = curriculumServices;
            _teamRepository = teamRepository;
            _sessionResultRepository = sessionResultRepository;
            _teamCurriculumRepository = teamCurriculumRepository;
        }


        public IEnumerable<SessionsWizardViewModel> GetCurriculumSessionsWizard(int teamId)
        {
            var sessions = _teamRepository.GetById(teamId).TeamCurriculum.Curriculum.Sessions.ToList();
            var sesResults = _teamRepository.GetById(teamId).TeamCurriculum.SessionResults;

            var team = _teamRepository.GetById(teamId).TeamCurriculum.Id;


            var result = from s in sessions

                         join sr in sesResults on s.Id equals sr == null ? 0 : sr.SessionId into leftm
                         from m in leftm.DefaultIfEmpty()

                         select new SessionsWizardViewModel()
                         {
                             Attendance = s.Attendance,
                             Name = s.Name,
                             Rating = s.Rating,
                             Number = s.Number,
                             NeedScenarios = s.NeedScenarios,
                             EndOfReviewPeriod = s.EndOfReviewPeriod,
                             Done = m == null ? false : m.Done,
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
                             StartedOn = m == null ? null : m.StartedOn,
                             ComletedOn = m == null ? null : m.ComletedOn,
                             SessionId = s.Id,
                             TeamCurriculumId = team
                         };
            return result.OrderBy(s => s.Number);
        }






    }
}
