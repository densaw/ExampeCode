using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;
using PmaPlus.Model.ViewModels.DashboardContent;

namespace PmaPlus.Controllers.ApiControllers.PLayers
{
    public class PlayerProfileController : ApiController
    {


        [Route("api/Player/Profile/CurriclumsScoreGraph/1")]
        public IEnumerable<GraphBoxViewModel> GeturriclumsScoreGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: Players curriculum score graph
        }



        [Route("api/Player/Profile/CurriclumsScoreGraph/2")]
        public IEnumerable<GraphBoxViewModel> GeturriclumsScoreGraph2()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: Players curriculum score graph
        }

        [Route("api/Player/Profile/SkillsProgress")]
        public InfoBoxViewModel GetSkillsProgress()
        {
            return null; //TODO: Skills progress for player
        }


        [Route("api/Player/Profile/SportSience")]
        public InfoBoxViewModel GetSportSience()
        {
            return null; //TODO: radar sport scientist for player
        }

    }
}
