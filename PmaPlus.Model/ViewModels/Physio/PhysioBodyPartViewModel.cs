using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Physio
{
    public class PhysioBodyPartViewModel
    {
        public int Id { get; set; }

        [Required]
        public BodyPartType Type { get; set; }
        [Required]
        public string InjuryName { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Treatment { get; set; }
    }
}
