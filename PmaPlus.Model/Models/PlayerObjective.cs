using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PlayerObjective
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int SessionResultId { get; set; }

        public string Objective { get; set; }
        public string Outcome { get; set; }
        public string FeedBack { get; set; }

        public virtual Player Player { get; set; }
        public virtual SessionResult SessionResult { get; set; }
    }
}
