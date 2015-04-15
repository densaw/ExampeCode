namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class SportScientist
    {
        public int Id { get; set; }

        public virtual Club Club { get; set; }

        public virtual User User { get; set; }
    }
}
