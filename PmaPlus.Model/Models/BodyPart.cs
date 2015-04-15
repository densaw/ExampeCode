namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class BodyPart
    {
       
       
        public int Id { get; set; }

        
        public int? Type  { get; set; }

        public string InjuryName { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Treatment { get; set; }

    }
}
