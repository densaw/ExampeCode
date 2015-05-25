using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumDetailPage : Page
    {
        public IEnumerable<CurriculumDetailViewModel> Items { get; set; }
    }
}
