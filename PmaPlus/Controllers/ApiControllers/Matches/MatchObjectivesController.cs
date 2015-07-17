using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Matches;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.Matches
{
    public class MatchObjectivesController : ApiController
    {
        private readonly MatchReportServices _matchReportServices;

        public MatchObjectivesController(MatchReportServices matchReportServices)
        {
            _matchReportServices = matchReportServices;
        }

        [Route("api/MatchObjectives/{matchId:int}")]
        public IEnumerable<PlayersMatchObjectiveTableViewModel> Get(int matchId)
        {
            return _matchReportServices.GetMatchObjectives(matchId);
        }

        [Route("api/MatchObjectives/Table")]
        public IHttpActionResult Post([FromBody] IList<PlayersMatchObjectiveTableViewModel> playersMatchObjectiveTable)
        {
            var obj =
                Mapper.Map<IList<PlayersMatchObjectiveTableViewModel>, List<PlayerMatchObjective>>(
                    playersMatchObjectiveTable);
            _matchReportServices.UpdateMatchObjectives(obj);
            return Ok();
        }

        public IHttpActionResult Post(PlayersMatchObjectiveTableViewModel playersMatchObjective)
        {
            var obj = Mapper.Map<PlayersMatchObjectiveTableViewModel, PlayerMatchObjective>(playersMatchObjective);
            _matchReportServices.AddMatchObjective(obj);
            return Ok();
        }

    }

    
}
