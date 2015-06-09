using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.DashboardContent;
using PmaPlus.Model.ViewModels.Player;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.Team;

namespace PmaPlus.Controllers.ApiControllers.TrainingTeamProfiles
{
    public class SportsScientistController : ApiController
    {

        [Route("api/TrainingTeam/SportsScientist/ClubInjuresGraph")]
        public IEnumerable<GraphBoxViewModel> GetClubInjuresGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: club injures graph
        }

        [Route("api/TrainingTeam/SportsScientist/ClubInjuresGraph")]
        public IEnumerable<PieChart> GetInjuryTypes()
        {
            return new List<PieChart>();
        }


        [Route("api/TrainingTeam/SportsScientist/Players/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
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

        [Route("api/TrainingTeam/SportsScientist/Teams/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
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
