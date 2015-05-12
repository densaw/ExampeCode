using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SiteSettings
{
    public class TargetViewModel
    {
        public int Id { get; set; }
        [Required]
        [Range( 1, 10000)]
        public int Target { get; set; }
        [Required]
        public decimal Value { get; set; }
    }
}
