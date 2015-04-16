using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public class Physiotherapist
    {

        public int Id { get; set; }

       
        public virtual ICollection<Club> Clubs { get; set; }

        public virtual User User { get; set; }
    }
}
