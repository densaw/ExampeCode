using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class PlayerBlockObjectiveTableViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }


        public string PreObjective { get; set; }
        public string Statement { get; set; }
        public bool Achieved { get; set; }
        
        
        
        public int Age { get; set; }
        public decimal Atl { get; set; }
        public decimal Att { get; set; }
        public int Frm { get;set; }
        public int Inj { get; set; }
        public decimal AttPercent { get; set; }
        public int WbPercent { get; set; }
        public decimal Cur { get; set; }





    }
}
