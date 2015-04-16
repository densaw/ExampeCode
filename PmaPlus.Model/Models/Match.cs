using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmaPlus.Model.Models
{
    public  class Match
    {
        
        public int Id { get; set; }

      

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        
        public string OppositionTeam { get; set; }

        public int? GoalsFor { get; set; }

        public int? GoalsAway { get; set; }

        public int? Type { get; set; }

        public virtual Team Team { get; set; }
    }
}
