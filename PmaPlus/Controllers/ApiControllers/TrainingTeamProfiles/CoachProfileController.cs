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
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.TrainingTeamProfiles
{
    public class CoachProfileController : ApiController
    {
        private readonly UserServices _userServices;
        private readonly CurriculumServices _curriculumServices;
        private readonly PlayerServices _playerServices;


        public CoachProfileController(UserServices userServices, CurriculumServices curriculumServices, PlayerServices playerServices)
        {
            _userServices = userServices;
            _curriculumServices = curriculumServices;
            _playerServices = playerServices;
        }


        [Route("api/TrainingTeam/Coach/MatchFormGraph")]
        public IEnumerable<GraphBoxViewModel> GetMatchFormGraph()
        {
            var temp = new List<GraphBoxViewModel>();

            for (int i = 0; i < 11; i++)
            {
                temp.Add(new GraphBoxViewModel() { Month = i, PlayersScore = 0 });
            }

            return temp; //TODO: Players score graph
        }

        [Route("api/TrainingTeam/Coach/TrainingTime")]
        public InfoBoxViewModel GetCoachTainingTime()
        {
            return null; //TODO: Trainig time of coach last week
        }



        [Route("api/TrainingTeam/Coach/MatchTime")]
        public InfoBoxViewModel GetCoachMachTime()
        {
            return null; //TODO: match time of coach last week
        }

        [Route("api/TrainingTeam/Coach/LoginFrequency")]
        public InfoBoxViewModel GetCoachLoginFrequency()
        {
            return null; //TODO: coach login frequency
        }

        [Route("api/TrainingTeam/Coach/Curiculums/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public CurriculumPage GetCurriculums(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {

            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            var count = _curriculumServices.GetClubCurriculums(clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var curriculums = _curriculumServices.GetClubCurriculums(clubId);
            var items = Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumTableViewModel>>(curriculums).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new CurriculumPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        [Route("api/TrainingTeam/Coach/Players/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public PlayersDetailPage GetPlayers(int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {
            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            var count = _playerServices.GetPlayersDetailTable(clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var items = _playerServices.GetPlayersDetailTable(clubId).OrderQuery(orderBy, x => x.Id, direction).Paged(pageNumber, pageSize);

            return new PlayersDetailPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }





    }
}
