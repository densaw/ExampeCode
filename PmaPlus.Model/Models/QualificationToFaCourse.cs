using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class QualificationToFaCourse
    {
        public int Id { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual FACourse FaCourse { get; set; }
    }
}
