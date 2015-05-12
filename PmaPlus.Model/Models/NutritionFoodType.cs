using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public  class NutritionFoodType
    {
       
      
        public int Id { get; set; }
        public FoodType Type { get; set; } 
        public string FoodName { get; set; }
        public virtual ICollection<FoodTypeToWhen> When { get; set; }

        public string GoodFor { get; set; }

       
        public string PortionSize { get; set; }

        public string Description { get; set; }
        public string Picture { get; set; }

        
        public decimal Calories { get; set; }

       
        public decimal CaloriesFromFat { get; set; }


        public decimal TotalFat { get; set; }

        public decimal SaturatedFat { get; set; }

        public decimal TransFat { get; set; }

        public decimal Cholesterol { get; set; }

        public decimal Sodium { get; set; }

        public decimal Potassium { get; set; }

        public decimal TotalCrbohydrate { get; set; }

        public decimal DietaryFibre { get; set; }

        public decimal Sugars { get; set; }

        public decimal Protein { get; set; }

        public decimal VitaminA { get; set; }

        public decimal VitaminB { get; set; }

        public decimal VitaminC { get; set; }

        public decimal VitaminD { get; set; }

        public decimal VitaminE { get; set; }

        public decimal Iron { get; set; }

        public decimal Calcium { get; set; }

        public decimal DailyIntake { get; set; }

    }
}
