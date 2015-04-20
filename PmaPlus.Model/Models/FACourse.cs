using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public class FACourse
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }
        public string Descriprion { get; set; }
    }
}
