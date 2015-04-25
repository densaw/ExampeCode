using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Physio
{
    public class PhysiotherapyExerciseViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Videolink { get; set; }

        public string Picture { get; set; }
    }
}
