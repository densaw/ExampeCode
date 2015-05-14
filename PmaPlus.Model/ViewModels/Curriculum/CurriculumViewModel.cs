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

        public int NumberOfBlocks { get; set; }

        public int NumberOfWeeks { get; set; }

        public int NumberOfSessions { get; set; }

        public int CurriculumTypeId { get; set; }

    }
}
