using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.TalentIdentifications
{
    public class AttributesOfTalentViewModel
    {
        public int TalentIdentificationId { get; set; }
        public int AttributeId { get; set; }
        public bool HaveAttribute { get; set; }
        public int Score { get; set; }
    }
}
