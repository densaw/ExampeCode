using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class TalentNote
    {
        public int Id { get; set; }

        public DateTime AddDate { get; set; }
        public string Scout { get; set; }
        public string Location { get;set; }
        public string Note { get; set; }

        public int TalentIdentificationId { get; set; }

        public virtual TalentIdentification TalentIdentification { get; set; }
    }
}
