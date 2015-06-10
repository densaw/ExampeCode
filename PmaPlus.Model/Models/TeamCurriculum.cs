using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class TeamCurriculum
    {
        public int Id { get; set; }
        public virtual Curriculum Curriculum { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int TrainingTime { get; set; }
        public int Progress { get; set; }

        public virtual ICollection<SessionResult> SessionResults { get; set; }
    }
}
