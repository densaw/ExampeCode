using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Team
{
    public class TeamTableForPhisiotherapistViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AvarageAge { get; set; }

        public int Inj { get; set; }

        public int PresentInj { get; set; }
        public decimal AttPercent { get; set; }
        public int WbPercent { get; set; }
        public decimal Cur { get; set; }
    }
}
