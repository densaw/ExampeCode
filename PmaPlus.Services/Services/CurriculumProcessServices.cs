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
        private readonly ISessionAttendanceRepository _sessionAttendanceRepository;
        private readonly IPlayerObjectiveRepository _playerObjectiveRepository;
        private readonly IPlayerBlockObjectiveRepository _playerBlockObjectiveRepository;
        private readonly IBlockObjectiveStatementRepository _blockObjectiveStatementRepository;
        private readonly IPlayerRatingsRepository _playerRatingsRepository;

        public CurriculumProcessServices(CurriculumServices curriculumServices, ITeamRepository teamRepository, ISessionResultRepository sessionResultRepository, ITeamCurriculumRepository teamCurriculumRepository, ISessionAttendanceRepository sessionAttendanceRepository, IPlayerObjectiveRepository playerObjectiveRepository, IPlayerBlockObjectiveRepository playerBlockObjectiveRepository, IBlockObjectiveStatementRepository blockObjectiveStatementRepository, IPlayerRatingsRepository playerRatingsRepository)
        {
            _curriculumServices = curriculumServices;
            _teamRepository = teamRepository;
            _sessionResultRepository = sessionResultRepository;
            _teamCurriculumRepository = teamCurriculumRepository;
            _sessionAttendanceRepository = sessionAttendanceRepository;
            _playerObjectiveRepository = playerObjectiveRepository;
            _playerBlockObjectiveRepository = playerBlockObjectiveRepository;
            _blockObjectiveStatementRepository = blockObjectiveStatementRepository;
            _playerRatingsRepository = playerRatingsRepository;
        }

        public IEnumerable<SessionsWizardViewModel> GetCurriculumSessionsWizard(int teamId)
        {
            var sessions = _teamRepository.GetById(teamId).TeamCurriculum.Curriculum.Sessions.ToList().OrderBy(s => s.Number);
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

        public void SaveSession(int sessionId, int teamCurriculumId)
        {
            //TODO: Check can we save session
            if (!_sessionResultRepository.GetMany(s => s.SessionId == sessionId && s.TeamCurriculumId == teamCurriculumId).Any())
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

        public IEnumerable<SessionAttendanceTableViewModel> GetPlayersTableForAttendance(int teamId, int sessionId)
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
                             Id = a != null ? a.Id : 0,
                             PlayerId = player.Id,
                             Picture = " /api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                             Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             Attendance = a != null ? a.Attendance : AttendanceType.Undefined,
                             Duration = a != null ? a.Duration : 0,
                             AttPercent = (player.SessionAttendances.Count / player.SessionAttendances.Count != 0 ? (player.SessionAttendances.Count(atten => atten.Attendance == AttendanceType.Attended)) : 1) * 100,
                             WbPercent = 0, //TODO: Wellbieng!
                             Cur = 0

                         };
            return result.AsEnumerable();
        }

        public void UpdateAttendance(List<SessionAttendance> attendanceTable, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            if (!team.TeamCurriculum.SessionResults.Any(s => s.SessionId == sessionId))
            {
                team.TeamCurriculum.SessionResults.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    TeamCurriculumId = team.TeamCurriculum.Id,

                });
            }

            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            attendanceTable.ForEach(a => a.SessionResultId = sessinResult.Id);

            _sessionAttendanceRepository.AddOrUpdate(attendanceTable.ToArray());

            //foreach (var attendance in attendanceTable)
            //{
            //    if (attendance.Id == 0)
            //    {
            //        _sessionAttendanceRepository.Add(attendance);
            //    }
            //    else
            //    {
            //        _sessionAttendanceRepository.Update(attendance);
            //    }
            //}
            //_sessionAttendanceRepository


        }

        public IEnumerable<PlayerObjectiveTableViewModel> GetPlayerObjectiveTable(int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players;
            ICollection<PlayerObjective> objectives = new List<PlayerObjective>();
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            if (sessionResult != null)
            {
                objectives = sessionResult.PlayerObjectives;
            }

            var result = from player in playres
                         join obj in objectives on player.Id equals obj == null ? 0 : obj.PlayerId into ob
                         from o in ob.DefaultIfEmpty()
                         select new PlayerObjectiveTableViewModel()
                         {
                             Id = o != null ? o.Id : 0,
                             PlayerId = player.Id,
                             Picture = " /api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                             Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             Objective = o != null ? o.Objective : "",
                             Outcome = o != null ? o.Outcome : "",
                             FeedBack = o != null ? o.FeedBack : ""
                         };
            return result.AsEnumerable();
        }

        public void UpdateObjectives(List<PlayerObjective> objectives, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            if (!team.TeamCurriculum.SessionResults.Any(s => s.SessionId == sessionId))
            {
                team.TeamCurriculum.SessionResults.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    TeamCurriculumId = team.TeamCurriculum.Id,

                });
            }

            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            objectives.ForEach(a => a.SessionResultId = sessinResult.Id);

            _playerObjectiveRepository.AddOrUpdate(objectives.ToArray());
        }



        public IEnumerable<PlayerBlockObjectiveTableViewModel> GetBlockObjectiveTable(int teamId, int sessionId, int userId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players;
            ICollection<PlayerBlockObjective> objectives = new List<PlayerBlockObjective>();
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            if (sessionResult != null)
            {
                objectives = sessionResult.PlayerBlockObjectives;
            }

            var result = from player in playres
                         join obj in objectives on player.Id equals obj == null ? 0 : obj.PlayerId into ob
                         from o in ob.DefaultIfEmpty()
                         let blockObjectiveStatement = o != null ? o.BlockObjectiveStatements.FirstOrDefault(b => b.UserId == userId) : null
                         select new PlayerBlockObjectiveTableViewModel()
                                  {
                                      Id = o != null ? o.Id : 0,
                                      PlayerId = player.Id,
                                      Picture = " /api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                                      Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                                      PreObjective = o != null ? o.PreObjective : "",
                                      Statement = blockObjectiveStatement != null ? blockObjectiveStatement.Statement : "",
                                      Achieved = blockObjectiveStatement != null ? blockObjectiveStatement.Achieved : false

                                      //TODO: table data finish!
                                  };

            return result.AsEnumerable();

        }

        public void UpdateBlockPreObjective(List<PlayerBlockObjective> blockObjectives, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            if (!team.TeamCurriculum.SessionResults.Any(s => s.SessionId == sessionId))
            {
                team.TeamCurriculum.SessionResults.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    TeamCurriculumId = team.TeamCurriculum.Id,

                });
            }

            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            blockObjectives.ForEach(a => a.SessionResultId = sessinResult.Id);

            _playerBlockObjectiveRepository.AddOrUpdate(blockObjectives.ToArray());

        }

        public void UpdateBlockObgectiveStatement(BlockObjectiveStatement blockObjectiveStatement, int playerId, int teamId, int sessionId, int userId)
        {
            var team = _teamRepository.GetById(teamId);
            if (!team.TeamCurriculum.SessionResults.Any(s => s.SessionId == sessionId))
            {
                team.TeamCurriculum.SessionResults.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    TeamCurriculumId = team.TeamCurriculum.Id,

                });
            }

            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            var blockObjective = sessinResult.PlayerBlockObjectives.FirstOrDefault(o => o.PlayerId == playerId);

            if (blockObjective == null)
                throw new Exception("block obj didn't created");

            var stmnt = blockObjective.BlockObjectiveStatements.FirstOrDefault(s => s.UserId == userId);


            if (stmnt == null)
            {
                blockObjectiveStatement.UserId = userId;
                blockObjectiveStatement.BlockObjectiveId = blockObjective.Id;
                blockObjective.BlockObjectiveStatements.Add(blockObjectiveStatement);

            }
            else
            {
                stmnt.Statement = blockObjectiveStatement.Statement;
                stmnt.Achieved = blockObjectiveStatement.Achieved;
                _blockObjectiveStatementRepository.Update(stmnt);
            }


        }




        public IEnumerable<PlayerRatingsTableViewModel> GetPlayerRatingsTable(int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players;
            ICollection<PlayerRatings> objectives = new List<PlayerRatings>();
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            if (sessionResult != null)
            {
                objectives = sessionResult.PlayerRatingses;
            }

            var result = from player in playres
                         join obj in objectives on player.Id equals obj == null ? 0 : obj.PlayerId into ob
                         from o in ob.DefaultIfEmpty()
                         select new PlayerRatingsTableViewModel()
                         {
                             Id = o != null ? o.Id : 0,
                             PlayerId = player.Id,
                             Picture = " /api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                             Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             Atl = o != null ? o.Atl : 0,
                             Att = o != null ? o.Att : 0,
                             Cur = o != null ? o.Cur : 0
                         };
            return result.AsEnumerable();
        }



        public void UpdatePlayersRating(List<PlayerRatings> playerRatingses, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            if (!team.TeamCurriculum.SessionResults.Any(s => s.SessionId == sessionId))
            {
                team.TeamCurriculum.SessionResults.Add(new SessionResult()
                {
                    SessionId = sessionId,
                    TeamCurriculumId = team.TeamCurriculum.Id,

                });
            }

            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            playerRatingses.ForEach(a => a.SessionResultId = sessinResult.Id);

            _playerRatingsRepository.AddOrUpdate(playerRatingses.ToArray());
        }

        public IEnumerable<CurriculumPlayersStatisticViewModel> CurriculumPlayersStatistic(int teamId)
        {
            throw new NotImplementedException();
        }
    }
}
