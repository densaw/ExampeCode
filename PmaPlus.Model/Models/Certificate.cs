using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmaPlus.Model.Models
{
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
