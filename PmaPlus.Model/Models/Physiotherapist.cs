namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class Physiotherapist
    {

        public int Id { get; set; }

       
        public virtual ICollection<Club> Clubs { get; set; }

        public virtual User User { get; set; }
    }
}
