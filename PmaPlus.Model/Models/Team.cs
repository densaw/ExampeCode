using System.Collections.Generic;


namespace PmaPlus.Model.Models
{
    public  class Team
    {
       
        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual Club Club { get; set; }

        public virtual ICollection<Coach> Coaches { get; set; }

        public virtual Curriculum Curriculum { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        
    }
}
