using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Nutrition
{
    public class NutritionRecipeTableViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal SaturatedFat { get; set; }
        public decimal TotalCarbohydrate { get; set; }
        public decimal Protein { get; set; }
    }
}
