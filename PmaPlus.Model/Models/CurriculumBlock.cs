using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public  class CurriculumBlock
    {
       

       
        public int Id { get; set; }


        public virtual CurriculumDetail CurriculumDetail { get; set; }

        public virtual Curriculum Curriculum { get; set; }

        public virtual ICollection<CurriculumWeek> CurriculumWeeks { get; set; }
    }
}
