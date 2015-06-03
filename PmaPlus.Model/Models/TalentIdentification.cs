using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class TalentIdentification
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateIdentificated { get; set; }
        public string PresentClub { get; set; }
        public DateTime BirthDate { get; set; }
        public string ParentsFirstName { get; set; }
        public string ParentsLastName { get; set; }
        public string ParentsEmail { get; set; }
        public string ParentsMobile { get; set; }
        public string ParentsNote { get; set; }

        public int ClubId { get; set; }
        public virtual Club Club { get; set; }

        public bool InvitedToTrial { get; set; }
        public bool AttendedTrail { get; set; }
        public bool JoinedClub { get; set; }


    }
}
