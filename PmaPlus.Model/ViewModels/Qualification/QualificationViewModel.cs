using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels.Qualification
{
    public class QualificationViewModel
    {
        public int Id { get; set; }
        public CertificateCourseType Type { get; set; }
        public string Name { get; set; }
        public int CertificateNumber { get; set; }
        public DateTime PassDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public virtual User User { get; set; }
    }
}
