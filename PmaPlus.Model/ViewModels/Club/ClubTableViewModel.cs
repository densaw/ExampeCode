using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model
{
    public class ClubTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TownCity { get; set; }
        public int Coaches { get; set; }
        public int Players { get; set; }
        public int PiPay { get; set; }
        public DateTime LastLogin { get; set; }
        public ClubStatus Status { get; set; }

    }
}
