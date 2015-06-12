using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Data.Repository.Iterfaces;
using PmaPlus.Model.Models;

namespace PmaPlus.Services.Services
{
    public class ScenarioServices
    {
        private readonly IScenarioRepository _scenarioRepository;

        public ScenarioServices(IScenarioRepository scenarioRepository)
        {
            _scenarioRepository = scenarioRepository;
        }

        public bool ScenarioExist(int id)
        {
            return _scenarioRepository.GetMany(s => s.Id == id).Any();
        }

        public IQueryable<Scenario> GetScenarios(string name, int clubId,bool isSysAdmin = false)
        {
            if (isSysAdmin)
            {
                return _scenarioRepository.GetAll();
            }
            return _scenarioRepository.GetMany(s => s.UploadedBy.ToLower() == name || s.UploadedBy.ToLower() == "SysAdmin" || s.ClubId == clubId || s.Share);
        }

        public Scenario GetScenarioById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return _scenarioRepository.GetById(id);
        }

        public Scenario AddScenario(Scenario scenario)
        {
            if (scenario == null)
            {
                return null;
            }
            return _scenarioRepository.Add(scenario);
        }

        public void UpdateScenario(Scenario scenario, int id)
        {
            if (id != 0)
            {
                scenario.Id = id;
                scenario.UploadedBy = _scenarioRepository.GetById(id).UploadedBy;
                _scenarioRepository.Update(scenario,id);
            }
        }

        public void DeleteScenario(int id)
        {
            if (id != 0)
            {
                _scenarioRepository.Delete(s => s.Id == id);
            }
        }

    }
}
