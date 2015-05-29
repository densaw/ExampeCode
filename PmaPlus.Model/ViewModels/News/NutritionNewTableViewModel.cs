using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.News
{
    public class NutritionNewTableViewModel
    {
        public int Id { get; set; }
        public DateTime NewsDate { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
    }
}
