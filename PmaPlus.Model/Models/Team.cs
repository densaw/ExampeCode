using System;
using System.Collections.Generic;


namespace PmaPlus.Model.Models
{
    public  class Team
    {

        public Team()
        {
            Coaches = new HashSet<Coach>();
            Players = new HashSet<Player>();
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public virtual Club Club { get; set; }

        public virtual ICollection<Coach> Coaches { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual TeamCurriculum TeamCurriculum { get; set; } 

    }
}
