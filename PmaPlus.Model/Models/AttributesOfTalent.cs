using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class AttributesOfTalent
    {
        [Key,Column(Order = 0)]
        public int TalentIdentificationId { get;set; }
        [Key, Column(Order = 1)]
        public int AttributeId { get; set; }
        public bool HaveAttribute { get; set; }
        public int Score { get; set; }

        public virtual TalentIdentification TalentIdentification { get; set; }
        public virtual PlayerAttribute Attribute { get; set; }
        
    }
}
