using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Curriculum;
using PmaPlus.Services;
using PmaPlus.Tools;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class CurriculumDetailsController : ApiController
    {
        private readonly CurriculumServices _curriculumServices;
        private readonly IPhotoManager _photoManager;



        public CurriculumDetailsController(CurriculumServices curriculumServices)
        {
            _curriculumServices = curriculumServices;
        }



        public CurriculumDetailViewModel Get(int id)
        {
            var curriculum = _curriculumServices.GetCurriculumDetails(id);
            return Mapper.Map<Curriculum, CurriculumDetailViewModel>(curriculum);
        } 

        public IHttpActionResult PostDetail(int id, [FromBody]CurriculumDetailViewModel detailViewModel)
        {
            var detail = Mapper.Map<CurriculumDetailViewModel, CurriculumDetail>(detailViewModel);
            var newDetail = _curriculumServices.AddCurriculumDetails(detail,detail.Id,detailViewModel.ScenarioId);
            if (newDetail == null)
            {
                return Conflict();
            }
            if (_photoManager.FileExists(detailViewModel.CurriculumDetailPlayersFriendlyPicture))
            {
                detailViewModel.CurriculumDetailPlayersFriendlyPicture =
                    _photoManager.MoveFromTemp(detailViewModel.CurriculumDetailPlayersFriendlyPicture,
                        FileStorageTypes.CurriculumDetails, newDetail.Id, "PlayersFriendlyPicture");
            }
            if (_photoManager.FileExists(detailViewModel.CurriculumDetailCoachPicture))
            {
                detailViewModel.CurriculumDetailCoachPicture =
                    _photoManager.MoveFromTemp(detailViewModel.CurriculumDetailCoachPicture,
                        FileStorageTypes.CurriculumDetails, newDetail.Id, "CoachPicture");
            }

            _curriculumServices.UpdateDetail(newDetail,newDetail.Id);
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody] CurriculumDetailViewModel detailViewModel)
        {
            var detail = Mapper.Map<CurriculumDetailViewModel, CurriculumDetail>(detailViewModel);

            if (_photoManager.FileExists(detailViewModel.CurriculumDetailPlayersFriendlyPicture))
            {
                detailViewModel.CurriculumDetailPlayersFriendlyPicture =
                    _photoManager.MoveFromTemp(detailViewModel.CurriculumDetailPlayersFriendlyPicture,
                        FileStorageTypes.CurriculumDetails, id, "PlayersFriendlyPicture");
            }
            if (_photoManager.FileExists(detailViewModel.CurriculumDetailCoachPicture))
            {
                detailViewModel.CurriculumDetailCoachPicture =
                    _photoManager.MoveFromTemp(detailViewModel.CurriculumDetailCoachPicture,
                        FileStorageTypes.CurriculumDetails, id, "CoachPicture");
            }

            _curriculumServices.UpdateDetail(detail,id);
            return Ok();

        }
    }
}
