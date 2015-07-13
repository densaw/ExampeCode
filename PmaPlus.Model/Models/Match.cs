using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public  class Match
    {
        public int Id { get; set; }
        public string OppositionName { get; set; }
        public MatchType Type { get; set; }
        public string Venue { get; set; }
        public string RiskAssessment { get; set; }
        public DateTime Date { get; set; }
        public SideTypes Side { get; set; }
        public string Formation { get; set; }
      

        public int GoalsFor { get; set; }
        public int GoalsAway { get; set; }
        public int Duration { get; set; }
        public int Periods { get; set; }
        public int PeriodDuration { get; set; }
        public bool Archived { get; set; }
        public string Notes { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public virtual ICollection<PlayerMatchObjective> PlayerMatchObjectives { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<PlayerMatchStatistic> MatchStatistics { get; set; }
        public virtual MatchMom MatchMom { get; set; }


    }
}
