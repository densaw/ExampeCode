using System;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model.PmaPlus.Model.Models;

namespace PmaPlus.Model.Models
{
    public  class PlayerInjury
    {
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InjuryDate { get; set; }


        public string InjuryName { get; set; }

        public string Stage { get; set; }

        public DateTime? RecoveryDate { get; set; }


        public virtual BodyPart BodyPart { get; set; }

        public virtual Player Player { get; set; }
    }
}
