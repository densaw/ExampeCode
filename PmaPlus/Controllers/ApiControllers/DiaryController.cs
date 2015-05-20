using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PmaPlus.Model.Models;
using PmaPlus.Model.ViewModels.Diary;
using PmaPlus.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class DiaryController : ApiController
    {
        private readonly DiaryServices _diaryServices;
        private readonly UserServices _userServices;

        public DiaryController(DiaryServices diaryServices, UserServices userServices)
        {
            _diaryServices = diaryServices;
            _userServices = userServices;
        }

        public IEnumerable<DiaryViewModel> GetUserDiaies()
        {
            var diaries = _diaryServices.GetUserDiaries(_userServices.GetUserByEmail(User.Identity.Name).Id);

            return Mapper.Map<IEnumerable<Diary>, IEnumerable<DiaryViewModel>>(diaries);
        }

        public IHttpActionResult Post([FromBody] AddDiaryViewModel diaryViewModel)
        {
            var diary = Mapper.Map<AddDiaryViewModel, Diary>(diaryViewModel);
            var userId = _userServices.GetUserByEmail(User.Identity.Name).Id;

            _diaryServices.AddDiary(diary, userId, diaryViewModel.SpecificPersons);
            return Ok();
        }


        public IHttpActionResult Delete(int id)
        {
            if (!_diaryServices.DiaryExist(id))
            {
                return NotFound();
            }
            _diaryServices.DeleteDiary(id);
            return Ok();
        }

    }
}
