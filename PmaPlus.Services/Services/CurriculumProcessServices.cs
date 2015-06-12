using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Enums;
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


        public void SaveSession(int sessionId,int teamCurriculumId)
        {
            //TODO: Check can we save session
            if (!_sessionResultRepository.GetMany(s =>s.SessionId == sessionId && s.TeamCurriculumId == teamCurriculumId).Any())
            {
                _sessionResultRepository.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    ComletedOn = DateTime.Now,
                    Done = true,
                    TeamCurriculumId = teamCurriculumId
                });
            }
        }


        public IEnumerable<SessionAttendanceTableViewModel> GetPlayersTableForAttendance(int teamId,int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players;
            ICollection<SessionAttendance> atendance = new List<SessionAttendance>();
            
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            if (sessionResult != null)
            {
                atendance = sessionResult.SessionAttendances;
            }
           
            var result = from player in playres
                    join attned in atendance on player.Id equals attned == null ? 0 : attned.PlayerId into att
                    from a in att.DefaultIfEmpty()
                    select new SessionAttendanceTableViewModel()
                    {
                        Id = player.User.Id,
                        Picture = player.User.UserDetail.ProfilePicture,
                        Attendance = a != null? a.Attendance : AttendanceType.Undefined,
                        Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                        AttPercent = 0,
                        WbPercent = 0,
                        Cur = 0

                    };
                return result.AsEnumerable();
            }

        public void UpdateAttendance(SessionAttendanceTableViewModel attendanceTable, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            if (!team.TeamCurriculum.SessionResults.Any(s =>s.SessionId == sessionId))
            {
                team.TeamCurriculum.SessionResults.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    TeamCurriculumId = team.TeamCurriculum.Id,
                    
                });
            }
        }
    }
}
