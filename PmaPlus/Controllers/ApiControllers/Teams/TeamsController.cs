using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Team;
using PmaPlus.Services;
using PmaPlus.Services.Services;
using WebGrease.Css.Extensions;

namespace PmaPlus.Controllers.ApiControllers.Teams
{
    public class TeamsController : ApiController
    {
        private readonly TeamServices _teamServices;
        private readonly UserServices _userServices;

        public TeamsController(TeamServices teamServices, UserServices userServices)
        {
            _teamServices = teamServices;
            _userServices = userServices;
        }

        [Route("api/Teams/List")]
        public IEnumerable<TeamsList> GetTeamsList()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return Mapper.Map<IEnumerable<Team>, IEnumerable<TeamsList>>(_teamServices.GetClubTeams(clubId));
        } 

        public IHttpActionResult GetTeams()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            var teams = _teamServices.GetClubTeams(clubId);
            var teamViewModel = Mapper.Map<IEnumerable<Team>, IEnumerable<TeamTableViewModel>>(teams);
            //teamViewModel.ForEach(t => t.); //TODO: CurriculumProgress for teams
            return Ok(teamViewModel);
        }


        public IHttpActionResult PostTeam([FromBody]AddTeamViewModel teamViewModel)
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            _teamServices.AddTeam(new Team() { Name = teamViewModel.Name }, clubId, teamViewModel.Players, teamViewModel.Coaches, teamViewModel.CurriculumId);
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody] AddTeamViewModel teamViewModel)
        {
            if (!_teamServices.TeamExist(id))
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
