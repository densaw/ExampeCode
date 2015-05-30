using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.OData.Edm.Library;
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

        [Route("api/Diary/ToDay")]
        public IEnumerable<DiaryViewModel> GetUserTodayDiaies()
        {
            var diaries = _diaryServices.GetUserDiaries(_userServices.GetUserByEmail(User.Identity.Name).Id);

            var today = DateTime.Now;

            diaries = from dr in diaries
                where dr.Start.Day == today.Day && dr.Start.Month == today.Month && dr.Start.Year == today.Year && (dr.Start > today || dr.End > today)
                select dr;

            return Mapper.Map<IEnumerable<Diary>, IEnumerable<DiaryViewModel>>(diaries.Take(2));
        }


        [Route("api/Diary/Future")]
        public IEnumerable<DiaryViewModel> GetUserFutureDiaies()
        {
            var diaries = _diaryServices.GetUserDiaries(_userServices.GetUserByEmail(User.Identity.Name).Id);

            var today = DateTime.Now;

            diaries = from dr in diaries
                      where dr.Start > today 
                      select dr;

            return Mapper.Map<IEnumerable<Diary>, IEnumerable<DiaryViewModel>>(diaries.Take(3));
        }




        public IEnumerable<DiaryViewModel> GetUserDiaies()
        {
            var diaries = _diaryServices.GetUserDiaries(_userServices.GetUserByEmail(User.Identity.Name).Id);

            return Mapper.Map<IEnumerable<Diary>, IEnumerable<DiaryViewModel>>(diaries);
        }

        public DiaryViewModel Get(int id)
        {
            return Mapper.Map<Diary, DiaryViewModel>(_diaryServices.GetDiaryById(id));
        }

        public IHttpActionResult Post([FromBody] AddDiaryViewModel diaryViewModel)
        {
            var diary = Mapper.Map<AddDiaryViewModel, Diary>(diaryViewModel);
            var userId = _userServices.GetUserByEmail(User.Identity.Name).Id;

            _diaryServices.AddDiary(diary, userId, diaryViewModel.SpecificPersons,diaryViewModel.AttendeeTypes);
            return Ok();
        }

        public IHttpActionResult Put(int id, [FromBody] AddDiaryViewModel diaryViewModel)
        {
            if (!_diaryServices.DiaryExist(id))
            {
                return NotFound();
            }

            var diary = Mapper.Map<AddDiaryViewModel, Diary>(diaryViewModel);
            var userId = _userServices.GetUserByEmail(User.Identity.Name).Id;

            _diaryServices.UpdateDiary(diary,id);
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
