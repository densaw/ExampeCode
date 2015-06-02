using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.DashboardContent;
using PmaPlus.Model.ViewModels.Player;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.Team;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.TrainingTeamProfiles
{
    public class PhysiotherapistsProfileController : ApiController
    {
        private readonly UserServices _userServices;

        public PhysiotherapistsProfileController(UserServices userServices)
        {
            _userServices = userServices;
        }


        [Route("api/TrainingTeam/Physiotherapist/MatchFormGraph")]
        public IEnumerable<PlayersScoreGraph> GetMatchFormGraph()
        {
            var temp = new List<PlayersScoreGraph>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new PlayersScoreGraph() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: coach match forms graph
        }

        [Route("api/TrainingTeam/Physiotherapist/Players/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public PlayersForPhisiotherapistPage GetPlayers(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {

            //var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            //var count = _curriculumServices.GetClubCurriculums(clubId).Count();
            //var pages = (int)Math.Ceiling((double)count / pageSize);
            //var curriculums = _curriculumServices.GetClubCurriculums(clubId);
            //var items = Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumTableViewModel>>(curriculums).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new PlayersForPhisiotherapistPage()
            {
                Count = 0,//count,
                Pages = 0, //pages,
                Items = new List<PlayerTableForPhisiotherapistViewModel>() // items
            }; //TODO: Players table for Phisiotherapist profile page

        }

        [Route("api/TrainingTeam/Physiotherapist/Teams/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public TeamsForPhisiotherapistPage GetTeams(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {

            //var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            //var count = _curriculumServices.GetClubCurriculums(clubId).Count();
            //var pages = (int)Math.Ceiling((double)count / pageSize);
            //var curriculums = _curriculumServices.GetClubCurriculums(clubId);
            //var items = Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumTableViewModel>>(curriculums).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new TeamsForPhisiotherapistPage()
            {
                Count = 0,//count,
                Pages = 0,//pages,
                Items = new List<TeamTableForPhisiotherapistViewModel>()//items
            };
            //TODO: Team table for Phisiotherapist profile page
        }


    }
}
