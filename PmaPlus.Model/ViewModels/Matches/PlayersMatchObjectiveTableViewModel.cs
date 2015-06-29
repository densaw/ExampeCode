using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Matches
{
    public class PlayersMatchObjectiveTableViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public string PlayerPicture { get; set; }
        public string PlayerName { get; set; }
        public string Objective { get; set; }
        public bool Outcome { get; set; }
    }
}
