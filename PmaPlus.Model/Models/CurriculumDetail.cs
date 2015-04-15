namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class CurriculumDetail
    {
       

       
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

      
        public string CoachDescription { get; set; }

        public string PlayersFriendlyName { get; set; }

        public string PlayersFriendlyPicture { get; set; }

        public string PlayersDescription { get; set; }

        public virtual Coach Coach { get; set; }
        public virtual ICollection<CurriculumBlock> CurriculumBlocks { get; set; }

        public virtual ICollection<Curriculum> Curriculums { get; set; }

        public virtual ICollection<CurriculumWeek> CurriculumWeeks { get; set; }
    }
}
