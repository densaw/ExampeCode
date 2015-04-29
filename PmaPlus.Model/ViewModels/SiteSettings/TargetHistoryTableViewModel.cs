using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SiteSettings
{
    public class TargetHistoryTableViewModel
    {
        public int Id { get; set; }
        public int Target { get; set; }
        public decimal Value { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
