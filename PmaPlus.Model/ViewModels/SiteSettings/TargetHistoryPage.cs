using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SiteSettings
{
    public class TargetHistoryPage:Page
    {
        public IEnumerable<TargetHistoryTableViewModel> Items { get; set; }
    }
}
