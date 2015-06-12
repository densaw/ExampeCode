using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class Player
    {

        public Player()
        {
            Teams = new HashSet<Team>();
        }
        public int Id { get; set; }

        public Foot PlayingFoot { get; set; }

        public string ParentsFirstName { get; set; }
        public string ParentsLastName { get; set; }
        public string ParentsContactNumber { get; set; }
        public string PlayerHealthConditions { get; set; }
        public string SchoolName { get; set; }
        public string SchoolTelephone { get; set; }
        public string SchoolContactName { get; set; }
        public string SchoolContactEmail { get; set; }
        public string SchoolAddress1 { get; set; }
        public string SchoolAddress2 { get; set; }
        public string SchoolTownCity { get; set; }
        public string SchoolPostcode { get; set; }

        public virtual Club Club { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual User User { get; set; }
        public UserStatus Status { get; set; }
        public virtual ICollection<PlayerInjury> PlayerInjuries { get; set; }

        public virtual ICollection<SessionAttendance> SessionAttendances { get; set; }
        public virtual ICollection<PlayerObjective> PlayerObjectives { get; set; }
    }
}
