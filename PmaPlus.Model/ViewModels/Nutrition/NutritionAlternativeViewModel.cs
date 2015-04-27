using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Nutrition
{
    public class NutritionAlternativeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string BadItemName { get; set; }
        [Required]
        public string AlternativeName { get; set; }
        [Required]
        public string BadItemPicture { get; set; }
        [Required]
        public string AlternativePicture { get; set; }
        [Required]
        public string BadItemDescription { get; set; }
        [Required]
        public string AlternativeDescription { get; set; }
        public string VideoLink { get; set; }
    }
}
