namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class NutritionAlternative
    {
        
        public int Id { get; set; }

       
        public string VideoLink { get; set; }


        public virtual NutritionFoodType NutritionFoodTypeBadItem { get; set; }

        public virtual NutritionFoodType NutritionFoodTypeAlternative { get; set; }
    }
}
