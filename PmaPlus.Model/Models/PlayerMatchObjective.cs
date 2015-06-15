using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PlayerMatchObjective
    {
        public int Id { get; set; }

        public string Objective { get; set; }
        public string Outcome { get; set; }

        public int MatchId { get; set; }
        public int PlayerId { get; set; }



        public virtual Player Player { get; set; }
        public virtual Match Match { get; set; }
    }
}
