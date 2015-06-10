using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class SessionsWizardViewModel
    {
        public int SessionId { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public bool Attendance { get; set; }
        public bool Objectives { get; set; }
        public bool Rating { get; set; }
        public bool Report { get; set; }
        public bool ObjectiveReport { get; set; }
        public bool CoachDetails { get; set; }
        public bool StartOfReviewPeriod { get; set; }
        public bool EndOfReviewPeriod { get; set; }
        public string CoachPicture { get; set; }
        public string CoachDetailsName { get; set; }
        public bool PlayerDetails { get; set; }
        public string PlayerPicture { get; set; }
        public string PlayerDetailsName { get; set; }
        public bool NeedScenarios { get; set; }


        public int TeamCurriculumId { get; set; }


        public DateTime? StartedOn { get; set; }

        public DateTime? ComletedOn { get; set; }

        public bool Done { get; set; }

    }
}
