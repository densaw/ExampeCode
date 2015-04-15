namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class CurriculumType
    {
       
        
        public int Id { get; set; }

        public bool? UsesBlocks { get; set; }

        public bool? UsesBlocksForAttendance { get; set; }

        public bool? UsesBlocksForObjectives { get; set; }

        public bool? UsesBlocksForRatings { get; set; }

        public bool? UsesBlocksForReports { get; set; }

        public bool? UsesWeeks { get; set; }

        public bool? UsesWeeksForAttendance { get; set; }

        public bool? UsesWeeksForObjectives { get; set; }

        public bool? UsesWeeksForRatings { get; set; }

        public bool? UsesWeeksForReports { get; set; }

        public bool? UsesSessions { get; set; }

        public bool? UsesSessionsForAttendance { get; set; }

        public bool? UsesSessionsForObjectives { get; set; }

        public bool? UsesSessionsForRatings { get; set; }

        public bool? UsesSessionsForReports { get; set; }

        public virtual ICollection<Curriculum> Curriculums { get; set; }
    }
}
