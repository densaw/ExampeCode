namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class Certificate
    {
        
        public int Id { get; set; }

        public int? CourseType { get; set; }

        public string CertificateName { get; set; }

        public int? CertificateNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PassDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }
    }
}
