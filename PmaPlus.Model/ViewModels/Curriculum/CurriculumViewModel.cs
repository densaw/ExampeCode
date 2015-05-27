using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AgeGroupType AgeGroup { get; set; }
    }
}
