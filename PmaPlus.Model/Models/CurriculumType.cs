using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public  class CurriculumType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool UsesBlocks { get; set; }
        [Required]
        public bool UsesBlocksForAttendance { get; set; }
        [Required]
        public bool UsesBlocksForObjectives { get; set; }
        [Required]
        public bool UsesBlocksForRatings { get; set; }
        [Required]
        public bool UsesBlocksForReports { get; set; }
        [Required]
        public bool UsesWeeks { get; set; }
        [Required]
        public bool UsesWeeksForAttendance { get; set; }
        [Required]
        public bool UsesWeeksForObjectives { get; set; }
        [Required]
        public bool UsesWeeksForRatings { get; set; }
        [Required]
        public bool UsesWeeksForReports { get; set; }
        [Required]
        public bool UsesSessions { get; set; }
        [Required]
        public bool UsesSessionsForAttendance { get; set; }
        [Required]
        public bool UsesSessionsForObjectives { get; set; }
        [Required]
        public bool UsesSessionsForRatings { get; set; }
        [Required]
        public bool UsesSessionsForReports { get; set; }
        
        public virtual ICollection<Curriculum> Curriculums { get; set; }
    }
}
