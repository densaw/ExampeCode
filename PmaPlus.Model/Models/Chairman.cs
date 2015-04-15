namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public class Chairman
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Telephone { get; set; }

        
    }
}
