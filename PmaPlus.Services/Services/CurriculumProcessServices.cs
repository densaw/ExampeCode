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


        public IEnumerable<Session> GetTeamSessions(int teamId)
        {
            return _teamRepository.GetById(teamId).TeamCurriculum.Curriculum.Sessions.ToList();
        }

        public IEnumerable<SessionResult> GetTeamSessionsResult(int teamId)
        {
            return _teamRepository.GetById(teamId).TeamCurriculum.SessionResults;
        }


        




    }
}
