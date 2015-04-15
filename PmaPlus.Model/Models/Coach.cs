namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

   
    public class Coach
    {
        
        public int Id { get; set; }

        

        public int? TeamId { get; set; }

        public virtual Club Club { get; set; }

        public virtual Team Team { get; set; }

        public virtual User User { get; set; }
    }
}
