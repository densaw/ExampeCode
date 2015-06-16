using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class SessionResult
    {
        public int Id { get; set; }

        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
        public int TeamCurriculumId { get; set; }
        public virtual TeamCurriculum TeamCurriculum { get; set; }

        public DateTime? StartedOn { get; set; }

        public DateTime? ComletedOn { get; set; }

        public bool Done { get; set; }


        public virtual ICollection<SessionAttendance> SessionAttendances { get; set; }
        public virtual ICollection<PlayerObjective> PlayerObjectives { get; set; }
        public virtual ICollection<PlayerBlockObjective> PlayerBlockObjectives { get; set; }
        public virtual ICollection<PlayerRatings> PlayerRatingses { get; set; }
    }
}
