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
        private readonly TeamRepository _teamRepository;
        private readonly ICurriculumRepository _curriculumRepository;
        private readonly ICoachRepository _coachRepository;
        private readonly IPlayerRepository _playerRepository;

        public TeamServices(TeamRepository teamRepository, IPlayerRepository playerRepository, ICoachRepository coachRepository, ICurriculumRepository curriculumRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _coachRepository = coachRepository;
            _curriculumRepository = curriculumRepository;
        }

        public IEnumerable<Team> GetClubTeams(int clubId)
        {
            return _teamRepository.GetMany(t => t.Club.Id == clubId);
        }


        public void AddTeam(Team team, int clubId, int[] playersId, int[] coachesId, int curriculumId)
        {
            var coaches = _coachRepository.GetMany(c => coachesId.Contains(c.Id));
            var players = _playerRepository.GetMany(p => playersId.Contains(p.Id));
            var curriculum = _curriculumRepository.GetById(curriculumId);
            if (coaches != null && players != null && curriculum != null)
            {
                foreach (var coach in coaches)
                {
                    team.Coaches.Add(coach);
                }
                foreach (var player in players)
                {
                    team.Players.Add(player);
                }
                team.Curriculum = curriculum;

                _teamRepository.Add(team);
            }

        }




    }
}
