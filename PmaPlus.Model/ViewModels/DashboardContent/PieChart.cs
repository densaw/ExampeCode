using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels
{
    public class PieChart
    {
        public BodyPartType Type { get; set; }
        public string Description { get;set; }
        public int Quantity { get; set; }
    }
}
