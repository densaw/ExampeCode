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
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers.Matches
{
    public class MatchReportsController : ApiController
    {
        private readonly MatchReportServices _matchReportServices;
        private readonly UserServices _userServices;
        private readonly IPhotoManager _photoManager;

        public MatchReportsController(MatchReportServices matchReportServices, UserServices userServices, IPhotoManager photoManager)
        {
            _matchReportServices = matchReportServices;
            _userServices = userServices;
            _photoManager = photoManager;
        }

        [Route("api/MatchReports/Archive/{matchId:int}")]
        public IHttpActionResult PutArchive(int matchId)
        {
            if (!_matchReportServices.MatchExist(matchId))
            {
                return NotFound();
            }

            _matchReportServices.ArchiveMatch(matchId);
            return Ok();
        }


        [Route("api/MatchReports/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public MatchesReportPage Get(int pageSize, int pageNumber, string orderBy = "", bool direction = true)
        {
            var user = _userServices.GetUserByEmail(User.Identity.Name);
            if (user == null)
                return null;

            IEnumerable<Match> maches;


            if (user.Role != Role.Coach)
            {
                var club = _userServices.GetClubByUserName(User.Identity.Name);
                maches = _matchReportServices.GetClubMatches(club.Id);
            }
            else
            {
                maches = _matchReportServices.GetCoachMatches(user.Id);
            }


            var count = maches.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var items = Mapper.Map<IEnumerable<Match>, IEnumerable<MatchReportTableViewModel>>(maches).OrderQuery(orderBy, x => x.Date, direction).Paged(pageNumber, pageSize);

            return new MatchesReportPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }


        public MatchReportViewModel Get(int id)
        {
            return Mapper.Map<Match, MatchReportViewModel>(_matchReportServices.GetMatchById(id));
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
            if (_photoManager.FileExists(matchReportViewModel.Picture))
            {
                matchReportViewModel.Picture = _photoManager.MoveFromTemp(matchReportViewModel.Picture,
                    FileStorageTypes.MatchReportPictures, id, "ReportPicture");
            }
            var match = Mapper.Map<MatchReportViewModel, Match>(matchReportViewModel);
            match.Id = id;
            match.Duration = match.Periods * match.PeriodDuration;
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
