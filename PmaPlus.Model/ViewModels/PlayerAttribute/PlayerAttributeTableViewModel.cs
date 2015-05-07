using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.PlayerAttribute
{
    public class PlayerAttributeTableViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AttributeType Type { get; set; }

        public int? MaxScore { get; set; }

        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }
    }
}
