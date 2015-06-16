using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PlayerMatchStatistic
    {
        [Key, Column(Order = 0)]
        public int PlayerId { get; set; }
        [Key, Column(Order = 1)]
        public int MatchId { get; set; }

        public int Goals { get; set; }
        public int Shots { get; set; }
        public int ShotsOnTarget { get; set; }
        public int Assists { get; set; }
        public int Tackles { get; set; }
        public int Passes { get; set; }
        public int Saves { get; set; }
        public int Corners { get; set; }
        public int FreeKicks { get; set; }
        public int FormRating { get; set; }
        public int PlayingTime { get; set; }


        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
}
