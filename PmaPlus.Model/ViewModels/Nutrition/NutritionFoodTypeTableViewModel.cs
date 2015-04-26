using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Nutrition
{
    public class NutritionFoodTypeTableViewModel
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public MealTime When { get; set; }
        public string PortionSize { get; set; }
        public string GoodFor { get; set; }
    }
}
