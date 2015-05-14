using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public  class CurriculumWeek
    {
        public int Id { get; set; }

        public virtual CurriculumBlock CurriculumBlock { get; set; }

        public virtual CurriculumDetail CurriculumDetail { get; set; }

        public virtual ICollection<CurriculumSession> CurriculumSessions { get; set; } 
    }
}
