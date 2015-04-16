using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public  class WelfareOfficer
    {

        public int Id { get; set; }

        public virtual ICollection<Club> Clubs { get; set; }
        
        public virtual User User { get; set; }
    }
}
