using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Matches;

namespace PmaPlus.Services.Services
{
    public class MatchReportServices
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerMatchObjectiveRepository _playerMatchObjectiveRepository;
        private readonly IPlayerMatchStatisticRepository _playerMatchStatisticRepository;
        private readonly IPlayerRepository _playerRepository;


        public MatchReportServices(IMatchRepository matchRepository, IPlayerMatchObjectiveRepository playerMatchObjectiveRepository, IPlayerMatchStatisticRepository playerMatchStatisticRepository, IPlayerRepository playerRepository)
        {
            _matchRepository = matchRepository;
            _playerMatchObjectiveRepository = playerMatchObjectiveRepository;
            _playerMatchStatisticRepository = playerMatchStatisticRepository;
            _playerRepository = playerRepository;
        }

        #region Maches
        
        public bool MatchExist(int id)
        {
            return _matchRepository.GetMany(m => m.Id == id).Any();
        }

        public IEnumerable<Match> GetCoachMatches(int coachUserId)
        {
            return _matchRepository.GetMany(m => m.Team.Coaches.Select(c => c.User.Id).Contains(coachUserId));
        }

        public IEnumerable<Match> GetClubMatches(int clubId)
        {
            return _matchRepository.GetMany(m => m.Team.Club.Id == clubId);
        }


        public Match GetMatchById(int id)
        {
            return _matchRepository.GetById(id);
        }

        public Match AddMatchReport(Match match)
        {
            return _matchRepository.Add(match);
        }

        public void UpdateMatchReport(Match match)
        {
            _matchRepository.Update(match);
        }

        public void DeleteMatchReport(int id)
        {
            _matchRepository.Delete(m => m.Id == id);
        }
        #endregion

        public IEnumerable<PlayersMatchObjectiveTableViewModel> GetMatchObjectives(int matchId)
        {
            var match = _matchRepository.GetById(matchId);
            if (match == null)
            {
                throw new Exception("no such match!");
            }
            var players = match.Team.Players;
            var matchObjectives = match.PlayerMatchObjectives;


            var result = from player in players
                join objective in matchObjectives on player.Id equals objective == null ? 0 : objective.PlayerId into
                    obj
                from o in obj.DefaultIfEmpty()
                select new PlayersMatchObjectiveTableViewModel()
                {
                    Id = o != null ? o.Id : 0,
                    PlayerId = player.Id,
                    PlayerName = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                    PlayerPicture ="/api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                    Objective = o != null ? o.Objective : "",
                    Outcome = o != null ? o.Outcome : OutcomeType.NotAchieved,
                    MatchId = matchId
                };
            return result;
        }

        public void UpdateMatchObjectives(List<PlayerMatchObjective> matchObjectives)
        {

            _playerMatchObjectiveRepository.AddOrUpdate(matchObjectives.ToArray());
        }


        public IEnumerable<PlayerMatchStatisticTableViewModel> GetPlayerMatchStatistics(int matchId)
        {
            var match = _matchRepository.GetById(matchId);
            if (match == null)
            {
                throw new Exception("no such match!");
            }
            var players = match.Team.Players;
            var matchStatistics = match.MatchStatistics;


            var result = from player in players
                         join statistic in matchStatistics on player.Id equals statistic == null ? 0 : statistic.PlayerId into
                             st
                         from s in st.DefaultIfEmpty()
                         select new PlayerMatchStatisticTableViewModel()
                         {
                             PlayerId = player.Id,
                             PlayerName = player.User.UserDetail.FirstName + " " + player.User.UserDetail.LastName,
                             PlayerPicture = "/api/file/ProfilePicture/" + player.User.UserDetail.ProfilePicture + "/" + player.User.Id,
                             MatchId = matchId,
                             Assists = s != null? s.Assists:0,
                             Corners = s != null ? s.Corners : 0,
                             FormRating = s != null ? s.FormRating : 0,
                             FreeKicks = s != null ? s.FreeKicks : 0,
                             Goals = s != null ? s.Goals : 0,
                             Passes = s != null ? s.Passes : 0,
                             PlayingTime = s != null ? s.PlayingTime : 0,
                             Saves = s != null ? s.Saves : 0,
                             Shots = s != null ? s.Shots : 0,
                             ShotsOnTarget = s != null ? s.ShotsOnTarget : 0,
                             Tackles = s != null ? s.Tackles : 0,
                         };
            return result;

        }

        public void UpdatePlayerMatchStatistic(List<PlayerMatchStatistic> matchStatistics)
        {
            _playerMatchStatisticRepository.AddOrUpdate(matchStatistics.ToArray());
        }

        public void AddPlayerMatchStatistic(PlayerMatchStatistic stat,bool manOfMatch = false)
        {
            if (_playerMatchStatisticRepository.GetMany( s => s.MatchId == stat.MatchId && s.PlayerId == stat.PlayerId).Any())
            {
                _playerMatchStatisticRepository.Update(stat);
            }
            else
            {
                _playerMatchStatisticRepository.Add(stat);
            }

            if (manOfMatch)
            {
                var match = _matchRepository.GetById(stat.MatchId);
                if (match != null)
                {
                    if (match.MatchMom == null)
                    {
                        match.MatchMom = new MatchMom()
                        {
                            MatchId = match.Id,
                            PlayerId = stat.PlayerId
                        };
                    }
                    else
                    {
                        match.MatchMom.PlayerId = stat.PlayerId;
                    }

                    _matchRepository.Update(match);
                }
            }
        }
    }
}
