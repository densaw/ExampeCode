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
        private readonly IClubRepository _clubRepository;

        public TeamServices(ITeamRepository teamRepository, IPlayerRepository playerRepository, ICoachRepository coachRepository, ICurriculumRepository curriculumRepository, ITeamCurriculumRepository teamCurriculumRepository, IClubRepository clubRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _coachRepository = coachRepository;
            _curriculumRepository = curriculumRepository;
            _teamCurriculumRepository = teamCurriculumRepository;
            _clubRepository = clubRepository;
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

            team.Club = _clubRepository.GetById(clubId);

            team.TeamCurriculum = new TeamCurriculum()
            {
                Curriculum = curriculum,
            };



            if (curriculum != null)
            {
                var newTeam = _teamRepository.Add(team);
                
                foreach (var coach in coaches)
                {
                    newTeam.Coaches.Add(coach);
                }
                foreach (var player in players)
                {
                    newTeam.Players.Add(player);
                }

                _teamRepository.Update(newTeam, newTeam.Id);
            }

        }


        public void UpdateTeam(string teamName, IList<int> playersId, IList<int> coachesId, int teamId)
        {
            var team = _teamRepository.GetById(teamId);
            team.Name = teamName;

            var coaches = _coachRepository.GetMany(c => coachesId.Contains(c.Id));
            var players = _playerRepository.GetMany(p => playersId.Contains(p.Id));

            foreach (var coach in coaches)
            {
                if (!team.Coaches.Contains(coach))
                {
                    team.Coaches.Add(coach);
                }
            }
            foreach (var player in players)
            {
                if (!team.Players.Contains(player))
                {
                    team.Players.Add(player);
                }
            }

            foreach (var item in team.Players.Where(p => !playersId.Contains(p.Id)).ToList())
            {
                team.Players.Remove(item);
            }

            foreach (var item in team.Coaches.Where(p => !coachesId.Contains(p.Id)).ToList())
            {
                team.Coaches.Remove(item);
            }

            _teamRepository.Update(team,team.Id);



        }

        public void DeleteTeam(int id)
        {
            _teamRepository.Delete(t => t.Id == id);
        }
    }
}
