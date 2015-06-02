using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels.Player
{
    public class PlayerTableForPhisiotherapistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Inj { get; set; }
        public int PresentInj { get; set; }
        public BodyPartType BodyPartType { get; set; }
        public decimal AttPercent { get; set; }
        public int WbPercent { get; set; }
        public decimal Cur { get; set; }

    }
}
