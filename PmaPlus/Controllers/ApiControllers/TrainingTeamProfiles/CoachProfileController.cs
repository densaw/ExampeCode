using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PmaPlus.Model.ViewModels;

namespace PmaPlus.Controllers.ApiControllers.TrainingTeamProfiles
{
    public class CoachProfileController : ApiController
    {


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

        [Route("api/TrainingTeam/Coach/")]
        public InfoBoxViewModel GetCoachLoginFrequency()
        {
            return null; //TODO: coach login frequency
        }



    }
}
