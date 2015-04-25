using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Physio
{
    public class PhysioBodyPartTableViewModel
    {
        public int Id { get; set; }

        public BodyPartType Type { get; set; }

        public string InjuryName { get; set; }
    }
}
