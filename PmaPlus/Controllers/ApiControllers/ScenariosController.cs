using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Data.Repository;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.SportsScience;
using PmaPlus.Services;
using PmaPlus.Services.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers
{
    public class ScenariosController : ApiController
    {
        private readonly ScenarioServices _scenarioServices;
        private readonly UserServices _userServices;
        private readonly IPhotoManager _photoManager;

        public ScenariosController(ScenarioServices scenarioServices, UserServices userServices, IPhotoManager photoManager)
        {
            _scenarioServices = scenarioServices;
            _userServices = userServices;
            _photoManager = photoManager;
        }

        // GET: api/Scenarios
        [Route("api/Scenarios/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public ScenarioPage Get(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _scenarioServices.GetScenarios().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var scenario = _scenarioServices.GetScenarios().OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<Scenario>, IEnumerable<ScenarioTableViewModel>>(scenario);

            return new ScenarioPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        // GET: api/Scenarios/5
        public ScenarioViewModel Get(int id)
        {
            return Mapper.Map<Scenario, ScenarioViewModel>(_scenarioServices.GetScenarioById(id));
        }

        // POST: api/Scenarios
        public IHttpActionResult Post([FromBody]ScenarioViewModel scenarioViewModel)
        {
            var scenario = Mapper.Map<ScenarioViewModel, Scenario>(scenarioViewModel);

            switch (_userServices.GetUserByEmail(User.Identity.Name).Role)
            {
                case Role.SystemAdmin:
                {
                    scenario.UploadedBy = "SysAdmin";
                    break;
                }
                case Role.ClubAdmin:
                {
                    scenario.UploadedBy = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Name;
                    break;
                }
                case Role.Coach:
                {
                    scenario.UploadedBy = "Coach";
                    break;
                }
                case Role.HeadOfAcademies:
                {
                    scenario.UploadedBy = "Head of Academy";
                    break;
                }
            }

            
            var newScenario = _scenarioServices.AddScenario(scenario);
            if (_photoManager.FileExists(newScenario.Picture))
            {
                newScenario.Picture = _photoManager.MoveFromTemp(newScenario.Picture, FileStorageTypes.Scenarios,
                    newScenario.Id, "ScenarioPicture");
                _scenarioServices.UpdateScenario(newScenario, newScenario.Id);
            }
            return Created(Request.RequestUri + newScenario.Id.ToString(), newScenario);
        }

        // PUT: api/Scenarios/5
        public IHttpActionResult Put(int id, [FromBody]ScenarioViewModel scenarioViewModel)
        {
            if (!_scenarioServices.ScenarioExist(id))
            {
                return NotFound();
            }
            var scenario = Mapper.Map<ScenarioViewModel, Scenario>(scenarioViewModel);
            if (_photoManager.FileExists(scenario.Picture))
            {
                scenario.Picture = _photoManager.MoveFromTemp(scenario.Picture, FileStorageTypes.Scenarios,
                    id, "ScenarioPicture");
            }

            _scenarioServices.UpdateScenario(scenario, id);
            return Ok();
        }

        // DELETE: api/Scenarios/5
        public IHttpActionResult Delete(int id)
        {
            if (!_scenarioServices.ScenarioExist(id))
            {
                return NotFound();
            }
            _scenarioServices.DeleteScenario(id);
            _photoManager.Delete(FileStorageTypes.Scenarios, id);
            return Ok();

        }
    }
}
