using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class TeamServices
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamCurriculumRepository _teamCurriculumRepository;

        public TeamServices(ITeamRepository teamRepository, IPlayerRepository playerRepository, ICoachRepository coachRepository, ICurriculumRepository curriculumRepository, ITeamCurriculumRepository teamCurriculumRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _coachRepository = coachRepository;
            _curriculumRepository = curriculumRepository;
            _teamCurriculumRepository = teamCurriculumRepository;
        }

        public bool TeamExist(int id)
        {
            return _teamRepository.GetMany(t => t.Id == id).Any();
        }

        public IQueryable<Team> GetClubTeams(int clubId)
        {
            return _teamRepository.GetMany(t => t.Club.Id == clubId);
        }


        public void AddTeam(Team team, int clubId, IList<int> playersId, IList<int> coachesId, int curriculumId)
        {
            var coaches = _coachRepository.GetMany(c => coachesId.Contains(c.Id));
            var players = _playerRepository.GetMany(p => playersId.Contains(p.Id));
            var curriculum = _curriculumRepository.GetById(curriculumId);
            if (curriculum != null)
            {
                foreach (var coach in coaches)
                {
                    team.Coaches.Add(coach);
                }
                foreach (var player in players)
                {
                    team.Players.Add(player);
                }

                var teamToCurr = new TeamCurriculum()
                {
                    Curriculum = curriculum,
                    Team = _teamRepository.Add(team)
                };
                _teamCurriculumRepository.Add(teamToCurr);

            }

        }

    }
}
