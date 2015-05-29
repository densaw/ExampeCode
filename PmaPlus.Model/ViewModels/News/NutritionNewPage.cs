using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.News
{
    public class NutritionNewPage : Page
    {
        public IEnumerable<NutritionNewTableViewModel> Items { get; set; }
    }
}
