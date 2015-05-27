using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumTableViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AgeGroupType AgeGroup { get; set; }

        public int SessionsCount { get; set; }

        public bool Started { get; set; }
    }
}
