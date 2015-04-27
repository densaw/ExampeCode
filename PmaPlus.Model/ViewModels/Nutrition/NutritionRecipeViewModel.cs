using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Nutrition
{
    public class NutritionRecipeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string IngridientsList { get; set; }
        public string Video { get; set; }
        [Required]
        public string Description { get; set; }
        public string Allergies { get; set; }
        [Required]
        public string Picture { get; set; }
        public string Serves { get; set; }
        public decimal Calories { get; set; }
        public decimal CaloriesFromFat { get; set; }
        public decimal TotalFat { get; set; }
        public decimal SaturatedFat { get; set; }
        public decimal TransFat { get; set; }
        public decimal Cholesterol { get; set; }
        public decimal Sodium { get; set; }
        public decimal Potassium { get; set; }
        public decimal TotalCarbohydrate { get; set; }
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
