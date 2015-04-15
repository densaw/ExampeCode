namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class CurriculumWeek
    {
        
        public int Id { get; set; }

     

        public virtual CurriculumBlock CurriculumBlock { get; set; }

        public virtual CurriculumDetail CurriculumDetail { get; set; }
    }
}
