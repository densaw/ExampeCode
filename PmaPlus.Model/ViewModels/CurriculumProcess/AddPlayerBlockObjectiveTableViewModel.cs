using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class AddPlayerBlockObjectiveTableViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }

        public string PreObjective { get; set; }
        
        public decimal AttPercent { get; set; }
        public int WbPercent { get; set; }
        public decimal Cur { get; set; }

    }
}
