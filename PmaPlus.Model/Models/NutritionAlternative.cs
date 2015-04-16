namespace PmaPlus.Model.Models
{
    public  class NutritionAlternative
    {
        
        public int Id { get; set; }

       
        public string VideoLink { get; set; }


        public virtual NutritionFoodType NutritionFoodTypeBadItem { get; set; }

        public virtual NutritionFoodType NutritionFoodTypeAlternative { get; set; }
    }
}
