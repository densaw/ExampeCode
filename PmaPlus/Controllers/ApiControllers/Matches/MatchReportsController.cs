using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using dotless.Core.Loggers;
using PmaPlus.Data;
using PmaPlus.Model;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Matches;
using PmaPlus.Services;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.Matches
{
    public class MatchReportsController : ApiController
    {
        private readonly MatchReportServices _matchReportServices;
        private readonly UserServices _userServices;

        public MatchReportsController(MatchReportServices matchReportServices, UserServices userServices)
        {
            _matchReportServices = matchReportServices;
            _userServices = userServices;
        }

        [Route("api/MatchReports/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public MatchesReportPage Get(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            if (user == null || user.Role != Role.Coach)
                return null;

            var maches = _matchReportServices.GetCoachMatches(user.Id);

            var count = maches.Count();
            var pages =(int)Math.Ceiling((double)count / pageSize);
            var items = Mapper.Map<IEnumerable<Match>, IEnumerable<MatchReportTableViewModel>>(maches).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new MatchesReportPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };


        }


        public MatchReportTableViewModel Get(int id)
        {
            return Mapper.Map<Match, MatchReportTableViewModel>(_matchReportServices.GetMatchById(id));
        }


        public IHttpActionResult Post(MatchReportViewModel matchReportViewModel)
        {
            var match = Mapper.Map<MatchReportViewModel, Match>(matchReportViewModel);
            _matchReportServices.AddMatchReport(match);
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody] MatchReportViewModel matchReportViewModel)
        {
            if (!_matchReportServices.MatchExist(id))
            {
                return NotFound();
            }

            var match = Mapper.Map<MatchReportViewModel, Match>(matchReportViewModel);
            match.Id = id;
            _matchReportServices.UpdateMatchReport(match);
            return Ok();

        }

        public IHttpActionResult Delete(int id)
        {
            if (!_matchReportServices.MatchExist(id))
            {
                return NotFound();
            }

            _matchReportServices.DeleteMatchReport(id);
            return Ok();
        }
    }
}
