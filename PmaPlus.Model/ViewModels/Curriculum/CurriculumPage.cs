using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumPage : Page
    {
        public IEnumerable<CurriculumViewModel> Items { get; set; }
    }
}
