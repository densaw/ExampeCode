using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Matches
{
    public class MatchReportTableViewModel
    {
        public int  Id { get; set; }
        public bool Won { get; set; }
        public string TeamName { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAway { get; set; }
        public DateTime Date { get; set; }
        public string OppositionName { get; set; }
        public string Mom { get; set; }
        public string Formation { get; set; }
        public string Picture { get; set; }
    }
}
