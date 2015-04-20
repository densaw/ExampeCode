using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumTypesTableViewModel
    {
        public string Name { get; set; }
        public int Blocks { get; set; }
        public string BlockATT { get; set; }
        public string BlockRTE { get; set; }
        public int Weeks { get; set; }
        public string WeekATT { get; set; }
        public string WeekRTE { get; set; }
        public int Sessions { get; set; }
        public string SessionsATT { get; set; }
        public string SessionsRTE { get; set; }
    }
}
