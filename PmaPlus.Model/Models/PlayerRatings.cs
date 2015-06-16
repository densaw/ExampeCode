using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PlayerRatings
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int SessionResultId { get; set; }

        public decimal Atl { get; set; }
        public decimal Att { get; set; }
        public decimal Cur { get; set; }

        public virtual Player Player { get; set; }
        public virtual SessionResult SessionResult { get; set; }
    }
}
