using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class MatchMom
    {
        [Key,ForeignKey("Match")]
        public int MatchId { get; set; }
        public int PlayerId { get; set; }

        public virtual Match Match { get; set; }
        public virtual Player Player { get; set; }
    }
}
