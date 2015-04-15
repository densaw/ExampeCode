namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class PlayerAttribute
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool? Type { get; set; }

        public int? MaxScore { get; set; }

        public int? AgeFrom { get; set; }

        public int? AgeTo { get; set; }
    }
}
