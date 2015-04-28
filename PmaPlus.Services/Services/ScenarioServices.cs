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

        public IQueryable<Scenario> GetScenarios()
        {
            return _scenarioRepository.GetAll();
        }

        public Scenario GetScenarioById(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return _scenarioRepository.GetById(id);
        }

        public Scenario AddScenario(Scenario scenario,string clubName = "SystemAdmin")
        {
            if (scenario == null)
            {
                return null;
            }

            scenario.UploadedBy = clubName;
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
