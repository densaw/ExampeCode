using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Physio
{
    public class PhysioExercisePage : Page
    {
        public IEnumerable<PhysiotherapyExerciseTableViewModel> Items { get; set; }
    }
}
