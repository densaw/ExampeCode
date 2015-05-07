using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.PlayerAttribute
{
    public class PlayerAttributeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public AttributeType Type { get; set; }

        public int? MaxScore { get; set; }
        [Required]
        public int AgeFrom { get; set; }
        [Required]
        public int AgeTo { get; set; }
    }
}
