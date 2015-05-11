using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class FoodTypeToType
    {
        public int Id { get; set; }
        public virtual NutritionFoodType FoodType { get; set; }
        public FoodType Type { get; set; }
    }
}
