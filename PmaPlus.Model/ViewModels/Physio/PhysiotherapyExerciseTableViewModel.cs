using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.Physio
{
    public class PhysiotherapyExerciseTableViewModel
    {
        public int Id { get; set; }

        public PhysioExerciseType Type { get; set; }

        public string Name { get; set; }
    }
}
