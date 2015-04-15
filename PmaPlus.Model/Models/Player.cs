namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Player
    {

        public int Id { get; set; }

        public virtual Club Club { get; set; }

        public virtual Team Team { get; set; }

        public virtual Team Team1 { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<PlayerInjury> PlayerInjuries { get; set; }
    }
}
