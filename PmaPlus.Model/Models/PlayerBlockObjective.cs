using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PlayerBlockObjective
    {
        public PlayerBlockObjective()
        {
            BlockObjectiveStatements = new HashSet<BlockObjectiveStatement>(); 
        }

        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int StartSessionResultId { get; set; }
        public int EndSessionResultId { get; set; }
        public string PreObjective { get; set; }
     
        public virtual Player Player { get; set; }
        public virtual SessionResult StartSessionResult { get; set; }
        public virtual SessionResult EndSessionResult { get; set; }
        public virtual ICollection<BlockObjectiveStatement> BlockObjectiveStatements { get; set; }

    }
}
