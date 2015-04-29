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
using PmaPlus.Model.ViewModels.SiteSettings;
using PmaPlus.Services;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers
{
    public class SiteSettingsController : ApiController
    {
        private readonly SiteSettingsServices _siteSettingsServices;
        private readonly UserServices _userServices;
        public SiteSettingsController(SiteSettingsServices siteSettingsServices, UserServices userServices)
        {
            _siteSettingsServices = siteSettingsServices;
            _userServices = userServices;
        }

        #region Targets

        [Route("api/TargetHistory/{pageSize:int}/{pageNumber:int}")]
        public TargetHistoryPage GetTargets(int pageSize, int pageNumber, string orderBy = "")
        {
            var count = _siteSettingsServices.GetTargetHistories().Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var targets = _siteSettingsServices.GetTargetHistories().Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<TargetHistory>, IEnumerable<TargetHistoryTableViewModel>>(targets);

            return new TargetHistoryPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }

        [Route("api/TargetHistory/")]
        public IHttpActionResult PostTarget(TargetViewModel target)
        {
            _siteSettingsServices.UpdateTarget(Mapper.Map<TargetViewModel,TargetHistory>(target));
            return Ok();
        }

        [Route("api/ActualTarget")]
        public TargetViewModel GetActualTaget()
        {
            return Mapper.Map<TargetHistory, TargetViewModel>(_siteSettingsServices.GetAtulalTareget());
        }
        #endregion

        #region UserPasswordHistory


        [Route("api/PasswordHistory/{pageSize:int}/{pageNumber:int}")]
        public PasswordHistoryPage GetPasswordHistory(int pageSize, int pageNumber, string orderBy = "")
        {
            var currentUser = _userServices.GetUserByEmail(User.Identity.Name);
            var count = _siteSettingsServices.GetPasswordHistory(currentUser).Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);
            var targets = _siteSettingsServices.GetPasswordHistory(currentUser).Paged(pageNumber, pageSize).AsEnumerable();
            var items = Mapper.Map<IEnumerable<PasswordHistory>, IEnumerable<PasswordHistoryTableViewModel>>(targets);

            return new PasswordHistoryPage()
            {
                Count = count,
                Pages = pages,
                Items = items
            };

        }
        [Route("api/UpdatePassword")]
        public IHttpActionResult PostNewPassword(PasswordViewModel password)
        {
            _siteSettingsServices.UpdatePassword(Mapper.Map<PasswordViewModel, PasswordHistory>(password), User.Identity.Name);
            return Ok();
        }

        #endregion



    }
}
