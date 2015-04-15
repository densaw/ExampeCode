namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
