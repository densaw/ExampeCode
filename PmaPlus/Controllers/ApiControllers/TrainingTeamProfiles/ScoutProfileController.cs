using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.DashboardContent;
using PmaPlus.Model.ViewModels.Player;

namespace PmaPlus.Controllers.ApiControllers.TrainingTeamProfiles
{
    public class ScoutProfileController : ApiController
    {


        [Route("api/TrainingTeam/Scout/ScoutedPlayersGraph")]
        public IEnumerable<GraphBoxViewModel> GetScoutedPlayersGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: scouted players graph
        }


        [Route("api/TrainingTeam/Scout/Players/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public PlayersForScoutPage GetPlayers(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {

            //var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            //var count = _curriculumServices.GetClubCurriculums(clubId).Count();
            //var pages = (int)Math.Ceiling((double)count / pageSize);
            //var curriculums = _curriculumServices.GetClubCurriculums(clubId);
            //var items = Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumTableViewModel>>(curriculums).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new PlayersForScoutPage()
            {
                Count = 0,//count,
                Pages = 0, //pages,
                Items = new List<PlayerTableForScoutViewModel>() // items
            }; //TODO: Players table for Scoute profile page

        }

        [Route("api/TrainingTeam/Scout/TalentIdentification")]
        public InfoBoxViewModel GetTalentIdentification()
        {
            return null; //TODO: Talent identification box
        }


    }
}
