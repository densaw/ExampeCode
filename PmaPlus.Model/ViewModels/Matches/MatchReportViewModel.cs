using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Matches
{
    public class MatchReportViewModel
    {
        public int Id { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public string OppositionName { get; set; }
        public MatchType Type { get; set; }
        [Required]
        public string Venue { get; set; }
        public string RiskAssessment { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public SideTypes Side { get; set; }
        [Required]
        public string Formation { get; set; }

        public string TeamName { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAway { get; set; }
        public int Duration { get; set; }
        public int Periods { get; set; }
        public int PeriodDuration { get; set; }

        public string Mom { get; set; }
        public string Notes { get; set; }

    }
}
