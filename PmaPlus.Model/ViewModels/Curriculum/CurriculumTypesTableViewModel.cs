using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumTypesTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Blocks { get; set; }
        public bool BlockATT { get; set; }
        public bool BlockRE { get; set; }
        public bool BlockRTE { get; set; }
        public bool Weeks { get; set; }
        public bool WeekATT { get; set; }
        public bool WeekRE { get; set; }
        public bool WeekRTE { get; set; }
        public bool Sessions { get; set; }
        public bool SessionsATT { get; set; }
        public bool SessionsRE { get; set; }
        public bool SessionsRTE { get; set; }
    }
}
