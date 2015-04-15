namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class CurriculumBlock
    {
       

       
        public int Id { get; set; }


        public virtual CurriculumDetail CurriculumDetail { get; set; }

        public virtual Curriculum Curriculum { get; set; }

        public virtual ICollection<CurriculumWeek> CurriculumWeeks { get; set; }
    }
}
