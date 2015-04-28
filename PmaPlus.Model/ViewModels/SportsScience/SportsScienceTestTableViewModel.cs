using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SportsScience
{
    public class SportsScienceTestTableViewModel
    {
        public int Id { get; set; }
        public SportsScienceType Type { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        public string LowValue { get; set; }
        public string HightValue { get; set; }
        public string NationalAverage { get; set; }
    }
}
