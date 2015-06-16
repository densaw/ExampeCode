using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Matches;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.Matches
{
    public class PlayerMatchStatisticController : ApiController
    {
        private readonly MatchReportServices _matchReportServices;

        public PlayerMatchStatisticController(MatchReportServices matchReportServices)
        {
            _matchReportServices = matchReportServices;
        }

        [Route("api/PlayerMatchStatistic/{matchId:int}")]
        public IEnumerable<PlayerMatchStatisticTableViewModel> Get(int matchId)
        {
            return _matchReportServices.GetPlayerMatchStatistics(matchId);
        }

        [Route("api/PlayerMatchStatistic/Table")]
        public IHttpActionResult Post([FromBody] IList<PlayerMatchStatisticTableViewModel> tableViewModels)
        {
            var stat = Mapper.Map<IList<PlayerMatchStatisticTableViewModel>, List<PlayerMatchStatistic>>(tableViewModels);
            var momPlayer = tableViewModels.FirstOrDefault(p => p.Mom);
            if (momPlayer != null)
            {
                _matchReportServices.AddPlayerMatchStatistic(Mapper.Map<PlayerMatchStatisticTableViewModel, PlayerMatchStatistic>(momPlayer), momPlayer.Mom);
            }
            _matchReportServices.UpdatePlayerMatchStatistic(stat);
            return Ok();
        }


        [Route("api/PlayerMatchStatistic/")]
        public IHttpActionResult PostSingle([FromBody] PlayerMatchStatisticTableViewModel statisticViewModels)
        {
            var stat = Mapper.Map<PlayerMatchStatisticTableViewModel, PlayerMatchStatistic>(statisticViewModels);
            _matchReportServices.AddPlayerMatchStatistic(stat, statisticViewModels.Mom);
            return Ok();
        }

    }
}
