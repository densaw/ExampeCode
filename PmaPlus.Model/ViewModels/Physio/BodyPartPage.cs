using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Physio
{
    public class BodyPartPage : Page
    {
        public IEnumerable<PhysioBodyPartTableViewModel> Items { get; set; }
    }
}
