using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Data;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Skill;
using PmaPlus.Model.ViewModels.TalentIdentifications;
using PmaPlus.Services;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.TalentIdentifications
{
    public class TalentPlayerAttributesController : ApiController
    {
        private readonly TalentServices _talentServices;
        private readonly UserServices _userServices;

        public TalentPlayerAttributesController(TalentServices talentServices, UserServices userServices)
        {
            _talentServices = talentServices;
            _userServices = userServices;
        }


        //public IEnumerable<AttributesOfTalentViewModel> Get(int id)
        //{
        //    var club = _userServices.GetClubByUserName(User.Identity.Name);

        //    if (club == null)
        //    {
        //        return null;
        //    }

        //    var attrbts = _talentServices.GetAttributesOfTalents(id, club.Id);
        //    return Mapper.Map<IEnumerable<AttributesOfTalent>, IEnumerable<AttributesOfTalentViewModel>>(attrbts);
        //}



        [Route("api/TalentPlayerAttributes/{talentId:int}/{pageSize:int}/{pageNumber:int}/{orderBy:alpha?}/{direction:bool?}")]
        public AttributesOfTalentPage Get(int talentId, int pageSize, int pageNumber, string orderBy = "", bool direction = false)
        {
            var clubId = _userServices.GetClubByUserName(User.Identity.Name).Id;

            var count = _talentServices.GetAttributesOfTalents(talentId, clubId).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var attrbts = _talentServices.GetAttributesOfTalents(talentId, clubId);
            var items = Mapper.Map<IEnumerable<AttributesOfTalent>, IEnumerable<AttributesOfTalentViewModel>>(attrbts).OrderQuery(orderBy, x => x.HaveAttribute, direction).Paged(pageNumber, pageSize);


            return new AttributesOfTalentPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };
        }



        public IHttpActionResult Post(AttributesOfTalentViewModel talentViewModel)
        {
            var attribute = Mapper.Map<AttributesOfTalentViewModel, AttributesOfTalent>(talentViewModel);

            if (attribute.HaveAttribute == true)
            {
                _talentServices.UpdateAttributesOfTalent(attribute);
            }
            else
            {
                _talentServices.DeleteAttributesOfTalent(attribute.TalentIdentificationId, attribute.AttributeId);
            }

            return Ok();
        }

    }
}
