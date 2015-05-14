using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public class Curriculum
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AgeGroupType AgeGroup { get; set; }

        public int NumberOfBlocks { get; set; }

        public int NumberOfWeeks { get; set; }

        public int NumberOfSessions { get; set; }

        public virtual ICollection<CurriculumBlock> CurriculumBlocks { get; set; }

        public virtual CurriculumDetail CurriculumDetail { get; set; }

        public int CurriculumTypeId { get; set; }

        public virtual CurriculumType CurriculumType { get; set; }

        public virtual Club Club { get; set; }
    }
}
