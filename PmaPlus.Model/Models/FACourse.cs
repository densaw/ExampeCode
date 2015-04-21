using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public class FACourse
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FACourse name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "FACourse duration period is required")]
        [Range(1, 365, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Duration { get; set; }
        public string Descriprion { get; set; }
    }
}
