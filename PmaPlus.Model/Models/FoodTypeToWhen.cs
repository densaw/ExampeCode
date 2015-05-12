using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class FoodTypeToWhen
    {
        public int Id { get; set; }
        public virtual NutritionFoodType FoodType { get; set; }
        public MealTime Type { get; set; }
    }
}
