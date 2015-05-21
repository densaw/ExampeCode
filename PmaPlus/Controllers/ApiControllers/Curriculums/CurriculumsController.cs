using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Model.ViewModels.Physio;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class CurriculumsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;
        private readonly UserServices _userServices;

        public CurriculumsController(CurriculumServices curriculumServices, UserServices userServices)
        {
            _curriculumServices = curriculumServices;
            _userServices = userServices;
        }

        [Route("api/Curriculums/List")]
        public IEnumerable<CurriculumsList> GetCurriculumsLists()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return _curriculumServices.GetClubCurriculumsList(clubId);
        }

        [Route("api/Curriculums/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}")]
        public CurriculumPage Get(int pageSize, int pageNumber, string orderBy = "")
        {

            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
          

            var count = _curriculumServices.GetClubCurriculums(clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var curriculums = _curriculumServices.GetClubCurriculums(clubId).OrderQuery(orderBy, f => f.Id).Paged(pageNumber, pageSize);
            var items = Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumViewModel>>(curriculums);

            return new CurriculumPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        public IEnumerable<CurriculumViewModel> Get()
        {
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            return Mapper.Map<IEnumerable<Curriculum>, IEnumerable<CurriculumViewModel>>(_curriculumServices.GetClubCurriculums(clubId));
        }


        public IHttpActionResult Post([FromBody] CurriculumViewModel curriculumViewModel)
        {
            var curriculum = Mapper.Map<CurriculumViewModel, Curriculum>(curriculumViewModel);
            var clubId = _userServices.GetClubAdminByUserName(User.Identity.Name).Club.Id;
            _curriculumServices.AddCurriculum(curriculum,clubId);
            return Ok();
        }



    }
}
