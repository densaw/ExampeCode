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


        public IEnumerable<SessionResult> GetCurriculumSessionsWizard(int teamId)
        {
            var team = _teamRepository.GetById(teamId);
            List<Session> sessions = team.TeamCurriculum.Curriculum.Sessions.OrderBy(s => s.Number).ToList();
            var sesResults = team.TeamCurriculum.SessionResults;
            sessions.ForEach(session =>
            {
                if (!sesResults.Any(r => r.SessionId == session.Id))
                {
                    sesResults.Add(new SessionResult()
                    {
                        SessionId = session.Id,
                        TeamCurriculumId = team.TeamCurriculum.Id,
                        Done = false
                    });
                }
            });
            _teamRepository.Update(team);


            return sesResults;
        }

        public void SaveSession(int sessionId, int teamId)
        {
            var team = _teamRepository.GetById(teamId);

            if (!team.TeamCurriculum.SessionResults.Any(s => s.SessionId == sessionId))
            {
                GetCurriculumSessionsWizard(teamId);
            }
            else
            {
                var session = _sessionResultRepository.Get(s => s.SessionId == sessionId);
                session.ComletedOn = DateTime.Now;
                session.Done = true;
                _sessionResultRepository.Update(session);
            }
        }

        #region Attendance
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
                             AttPercent = ((decimal)player.SessionAttendances.Count(atten => atten.Attendance == AttendanceType.Attended) / (player.SessionAttendances.Count != 0 ? player.SessionAttendances.Count : 1)) * 100,
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
        #endregion


        #region Players Objective
        public IEnumerable<PlayerObjective> GetPlayerObjectiveTableForAdd(int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players.ToList();
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            SessionResult endSessionResult = null;

            #region find end
            bool startFound = false;
            foreach (var sessionR in team.TeamCurriculum.SessionResults)
            {
                if (startFound)
                {
                    if (sessionR.Session.ObjectiveReport)
                    {
                        endSessionResult = sessionR;
                        break;
                    }
                }
                else
                {
                    if (sessionR.SessionId == sessionId)
                    {
                        startFound = true;
                    }

                }

            }
            if (endSessionResult == null)
            {
                throw new Exception("Not found end of Objective report");
            }
            #endregion

            if (sessionResult == null)
            {
                GetCurriculumSessionsWizard(teamId);
            }
            var objectives = sessionResult.StartPlayerObjectives;

            playres.ForEach(player =>
            {
                if (!objectives.Any(o => o.PlayerId == player.Id))
                {
                    objectives.Add(new PlayerObjective()
                    {
                        PlayerId = player.Id,
                        StartSessionResultId = sessionId,
                        EndSessionResultId = endSessionResult.Id
                    });
                }
            });

            _sessionResultRepository.Update(sessionResult);

            return objectives;
        }

        public void AddObjectives(IList<AddPlayerObjectiveTableViewModel> newObjectives, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            var objectives = sessinResult.StartPlayerObjectives.ToList();

            objectives.ForEach(objective =>
            {
                var obj = newObjectives.FirstOrDefault(o => o.PlayerId == objective.PlayerId);
                if (obj != null)
                    objective.Objective = obj.Objective;
            });
            _playerObjectiveRepository.AddOrUpdate(objectives.ToArray());
        }

        public IEnumerable<PlayerObjective> GetPlayerObjectiveTableForReport(int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);

            if (sessionResult == null)
            {
                GetCurriculumSessionsWizard(teamId);
            }
            var objectives = sessionResult.EndPlayerObjectives;

            return objectives;
        }

        public void AddReportObjectives(IList<PlayerObjectiveTableViewModel> playerObjectiveTable, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            var objectives = sessinResult.EndPlayerObjectives.ToList();

            objectives.ForEach(objective =>
            {
                var obj = playerObjectiveTable.FirstOrDefault(o => o.PlayerId == objective.PlayerId);
                if (obj != null)
                    objective.Outcome = obj.Outcome;
            });
            _playerObjectiveRepository.AddOrUpdate(objectives.ToArray());

        }
        #endregion

        #region BloCk Objective

        public IEnumerable<PlayerBlockObjective> GetBlockObjectiveTableForAdd(int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var playres = team.Players.ToList();
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);
            SessionResult endSessionResult = null;

            #region find end
            bool startFound = false;
            foreach (var sessionR in team.TeamCurriculum.SessionResults)
            {
                if (startFound)
                {
                    if (sessionR.Session.EndOfReviewPeriod)
                    {
                        endSessionResult = sessionR;
                        break;
                    }
                }
                else
                {
                    if (sessionR.SessionId == sessionId)
                        startFound = true;
                }
            }
            if (endSessionResult == null)
                throw new Exception("Not found end of Objective report");
            #endregion


            if (sessionResult == null)
            {
                GetCurriculumSessionsWizard(teamId);
            }
            var objectives = sessionResult.StartPlayerBlockObjectives;

            playres.ForEach(player =>
            {
                if (!objectives.Any(o => o.PlayerId == player.Id))
                {
                    objectives.Add(new PlayerBlockObjective()
                    {
                        PlayerId = player.Id,
                        StartSessionResultId = sessionId,
                        EndSessionResultId = endSessionResult.Id

                    });
                }
            });

            _sessionResultRepository.Update(sessionResult);


            return objectives;

        }

        public void AddBlockPreObjectives(IList<AddPlayerBlockObjectiveTableViewModel> newblockObjectives, int teamId, int sessionId)
        {
            var team = _teamRepository.GetById(teamId);
            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            var blockObjectives = sessinResult.StartPlayerBlockObjectives.ToList();

            blockObjectives.ForEach(objective =>
            {
                var preObj = newblockObjectives.FirstOrDefault(o => o.PlayerId == objective.PlayerId);
                if (preObj != null)
                    objective.PreObjective = preObj.PreObjective;
            });
            _playerBlockObjectiveRepository.AddOrUpdate(blockObjectives.ToArray());
        }

        public IEnumerable<PlayerBlockObjectiveTableViewModel> GetBlockObjectiveTableForReport(int teamId, int sessionId,int userId)
        {
            var team = _teamRepository.GetById(teamId);
            var sessionResult = team.TeamCurriculum.SessionResults.FirstOrDefault(s => s.SessionId == sessionId);

            if (sessionResult == null)
            {
                GetCurriculumSessionsWizard(teamId);
            }
            var objectives = sessionResult.EndPlayerBlockObjectives;

            return from objective in objectives
                select new PlayerBlockObjectiveTableViewModel()
                {
                    Id = objective.Id,
                    Name = objective.Player.User.UserDetail.FirstName + " " + objective.Player.User.UserDetail.LastName,
                    Picture =
                        "/api/file/ProfilePicture/" + objective.Player.User.UserDetail.ProfilePicture + "/" +
                        objective.Player.User.Id,
                    PlayerId = objective.PlayerId,
                    PreObjective = objective.PreObjective,
                    Statement =
                        objective.BlockObjectiveStatements.FirstOrDefault(s => s.UserId == userId) != null
                            ? objective.BlockObjectiveStatements.FirstOrDefault(s => s.UserId == userId).Statement
                            : "",
                    Achieved =
                        objective.BlockObjectiveStatements.FirstOrDefault(s => s.UserId == userId) != null
                            ? objective.BlockObjectiveStatements.FirstOrDefault(s => s.UserId == userId).Achieved
                            : false,
                };
        }

        public void ReportBlockPreObjectives(IList<PlayerBlockObjectiveTableViewModel> playerObjectivesTable, int teamId, int sessionId,int userId)
        {
            var team = _teamRepository.GetById(teamId);
            var sessinResult = team.TeamCurriculum.SessionResults.FirstOrDefault(sr => sr.SessionId == sessionId);

            if (sessinResult == null)
                throw new Exception("Session didn't created");

            var objectives = sessinResult.EndPlayerBlockObjectives.ToList();

            objectives.ForEach(objective =>
            {
                var obj = playerObjectivesTable.FirstOrDefault(o => o.PlayerId == objective.PlayerId);
                if (obj != null)
                {
                    var state = objective.BlockObjectiveStatements.FirstOrDefault(s => s.UserId == userId);
                    if (state == null)
                    {
                        objective.BlockObjectiveStatements.Add(new BlockObjectiveStatement()
                        {
                            UserId = userId,
                            Achieved = obj.Achieved,
                            Statement = obj.Statement,
                            BlockObjectiveId = objective.Id
                        });
                    }
                    else
                    {
                        state.Achieved = obj.Achieved;
                        state.Statement = obj.Statement;
                    }

                }
            });
            _playerBlockObjectiveRepository.AddOrUpdate(objectives.ToArray());
        }


        #endregion

        #region Player Ratings
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
                             Picture = "/api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
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
        #endregion

        public IEnumerable<CurriculumPlayersStatisticViewModel> CurriculumPlayersStatistic(int teamId)
        {
            var team = _teamRepository.GetById(teamId);
            var players = team.Players;

            var result = from player in players
                         select new CurriculumPlayersStatisticViewModel()
                                  {
                                      PlayerPicture = "/api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
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
                                      AttPercent = ((decimal)player.SessionAttendances.Count(a => a.Attendance == AttendanceType.Attended) / (player.SessionAttendances.Count != 0 ? player.SessionAttendances.Count : 1)) * 100
                                  };

            return result.AsEnumerable();

        }


        public void ArchiveTeam(int teamId)
        {
            var team = _teamRepository.GetById(teamId);
            team.TeamCurriculum.Archived = true;
            _teamRepository.Update(team);
        }
    }
}
