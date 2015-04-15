namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    
    public class Curriculum
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public int? AgeGroup { get; set; }

        public int? NumberOfBlocks { get; set; }

        public int? NumberOfWeeks { get; set; }

        public int? NumberOfSessions { get; set; }

        public virtual ICollection<CurriculumBlock> CurriculumBlocks { get; set; }

        public virtual CurriculumDetail CurriculumDetail { get; set; }

        public virtual CurriculumType CurriculumType { get; set; }

        public virtual ICollection<Team> Teams { get; set; }
    }
}
