using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.TalentIdentifications
{
    public class TalentIdentificationTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScouteName { get; set; }
        public decimal Score { get; set; }
        public bool PresentInClub { get; set; }
        public DateTime DateIdentificated { get; set; }
        public string PresentClub { get; set; }
        public int  Age { get; set; }
        public string ParentsMobile { get; set; }

        public bool InvitedToTrial { get; set; }
        public bool AttendedTrail { get; set; }
        public bool JoinedClub { get; set; }
    }
}
