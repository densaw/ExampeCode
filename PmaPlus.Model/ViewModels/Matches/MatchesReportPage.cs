using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Matches
{
    public class MatchesReportPage:Page
    {
        public IEnumerable<MatchReportTableViewModel> Items { get; set; }
    }
}
