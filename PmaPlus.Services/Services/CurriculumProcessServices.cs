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
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamCurriculumRepository _teamCurriculumRepository;
        private readonly ISessionResultRepository _sessionResultRepository;
        private readonly ISessionAttendanceRepository _sessionAttendanceRepository;
        private readonly IPlayerObjectiveRepository _playerObjectiveRepository;
        private readonly IPlayerBlockObjectiveRepository _playerBlockObjectiveRepository;
        private readonly IBlockObjectiveStatementRepository _blockObjectiveStatementRepository;
        private readonly IPlayerRatingsRepository _playerRatingsRepository;

        public CurriculumProcessServices(ITeamRepository teamRepository, ITeamCurriculumRepository teamCurriculumRepository, ISessionResultRepository sessionResultRepository, ISessionAttendanceRepository sessionAttendanceRepository, IPlayerObjectiveRepository playerObjectiveRepository, IPlayerBlockObjectiveRepository playerBlockObjectiveRepository, IBlockObjectiveStatementRepository blockObjectiveStatementRepository, IPlayerRatingsRepository playerRatingsRepository)
        {
            _teamRepository = teamRepository;
            _teamCurriculumRepository = teamCurriculumRepository;
            _sessionResultRepository = sessionResultRepository;
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
                             CoachPicture = "/api/file/Sessions/" + s.CoachPicture + "/" + s.Id,
                             PlayerDetails = s.PlayerDetails,
                             PlayerDetailsName = s.PlayerDetailsName,
                             PlayerPicture = "/api/file/Sessions/" + s.PlayerPicture + "/" + s.Id,
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
            else
            {
                var session = _sessionResultRepository.Get(s => s.SessionId == sessionId && s.TeamCurriculumId == teamCurriculumId);
                session.ComletedOn = DateTime.Now;
                session.Done = true;
                _sessionResultRepository.Update(session);
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
                             Picture = "/api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                             Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             Attendance = a != null ? a.Attendance : AttendanceType.Undefined,
                             Duration = a != null ? a.Duration : 0,
                             AttPercent = (player.SessionAttendances.Count(atten => atten.Attendance == AttendanceType.Attended) / (player.SessionAttendances.Count != 0 ? player.SessionAttendances.Count : 1)) * 100,
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


        public IEnumerable<PlayerObjectiveTableViewModel> GetPlayerObjectiveRepTable(int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players;
            ICollection<PlayerObjective> objectives = new List<PlayerObjective>();
            ICollection<PlayerObjective> lastObjectives = new List<PlayerObjective>();


            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            var lastObjSessionResult =
                team.TeamCurriculum.SessionResults.OrderBy(s => s.Session.Number)
                    .LastOrDefault(o => o.Session.Objectives);



            if (sessionResult != null)
            {
                objectives = sessionResult.PlayerObjectives;
            }


            if (lastObjSessionResult != null)
            {
                lastObjectives = lastObjSessionResult.PlayerObjectives;
            }

            var result = from player in playres
                         join obj in objectives on player.Id equals obj == null ? 0 : obj.PlayerId into ob
                         from o in ob.DefaultIfEmpty()
                         join lobj in lastObjectives on player.Id equals lobj ==  null ? 0 : lobj.PlayerId into lob
                         from lo in lob.DefaultIfEmpty()
                         select new PlayerObjectiveTableViewModel()
                         {
                             Id = o != null ? o.Id : 0,
                             PlayerId = player.Id,
                             Picture = " /api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                             Name = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             Objective = lo != null ? lo.Objective : "",
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

            objectives.ForEach(a => a.StartSessionResultId = sessinResult.Id);

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
                                      Achieved = blockObjectiveStatement != null ? blockObjectiveStatement.Achieved : false,
                                      Age = DateTime.Now.Year - (player.User.UserDetail.Birthday.HasValue ? player.User.UserDetail.Birthday.Value.Year : DateTime.Now.Year),
                                      Atl = player.PlayerRatingses.Select(r => r.Atl).DefaultIfEmpty().Average(),
                                      Att = player.PlayerRatingses.Select(r => r.Att).DefaultIfEmpty().Average(),
                                      Frm = player.MatchStatistics.Select(m => m.FormRating).DefaultIfEmpty().Average(),
                                      Inj = player.PlayerInjuries.Count,
                                      Cur = player.PlayerRatingses.Select(r => r.Cur).DefaultIfEmpty().Average(),
                                      AttPercent = (player.SessionAttendances.Count(a => a.Attendance == AttendanceType.Attended) / (player.SessionAttendances.Count != 0 ? player.SessionAttendances.Count : 1)) * 100,
                                      PlaingTime = player.MatchStatistics.Select(m => m.PlayingTime).DefaultIfEmpty().Sum(),
                                      TrainigTime = player.SessionAttendances.Select(s =>s.Duration).Sum()

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
            var team = _teamRepository.GetById(teamId);
            var players = team.Players;

            var result = from player in players
                select new CurriculumPlayersStatisticViewModel()
                         {
                             PlayerName = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             Age = DateTime.Now.Year - (player.User.UserDetail.Birthday.HasValue ? player.User.UserDetail.Birthday.Value.Year : DateTime.Now.Year),
                             Atl = player.PlayerRatingses.Select(r => r.Atl).DefaultIfEmpty().Average(),
                             Att = player.PlayerRatingses.Select(r => r.Att).DefaultIfEmpty().Average(),
                             Mom = player.MatchMoms.Count,
                             Gls = player.MatchStatistics.Select(m => m.Goals).DefaultIfEmpty().Average(),
                             Sho = player.MatchStatistics.Select(m => m.Shots).DefaultIfEmpty().Average(),
                             Sht = player.MatchStatistics.Select(m => m.ShotsOnTarget).DefaultIfEmpty().Average(),
                             Asi = player.MatchStatistics.Select(m => m.Assists).DefaultIfEmpty().Average(),
                             Tck = player.MatchStatistics.Select(m => m.Tackles).DefaultIfEmpty().Average(),
                             Pas = player.MatchStatistics.Select(m => m.Passes).DefaultIfEmpty().Average(),
                             Sav = player.MatchStatistics.Select(m => m.Saves).DefaultIfEmpty().Average(),
                             Crn = player.MatchStatistics.Select(m => m.Corners).DefaultIfEmpty().Average(),
                             Frk = player.MatchStatistics.Select(m => m.FreeKicks).DefaultIfEmpty().Average(),
                             Frm = player.MatchStatistics.Select(m => m.FormRating).DefaultIfEmpty().Average(),
                             Inj = player.PlayerInjuries.Count,
                             Cur = player.PlayerRatingses.Select(r => r.Cur).DefaultIfEmpty().Average(),
                             AttPercent = (player.SessionAttendances.Count(a => a.Attendance == AttendanceType.Attended) / (player.SessionAttendances.Count != 0 ? player.SessionAttendances.Count : 1)) * 100
                         };

            return result.AsEnumerable();

        }
    }
}
