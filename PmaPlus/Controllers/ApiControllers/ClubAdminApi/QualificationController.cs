using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Services.Services;

namespace PmaPlus.Controllers.ApiControllers.ClubAdminApi
{
    public class QualificationController : ApiController
    {
        private readonly TrainingTeamMembersServices _teamMembersServices;

        public QualificationController(TrainingTeamMembersServices teamMembersServices)
        {
            _teamMembersServices = teamMembersServices;
        }
    }
}
