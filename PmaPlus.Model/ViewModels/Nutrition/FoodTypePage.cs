using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Nutrition
{
    public class FoodTypePage : Page
    {
        public IEnumerable<NutritionFoodTypeTableViewModel> Items { get; set; }
    }
}
