using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public class Chairman
    {
        
        public int Id { get; set; }

        
        public string Name { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(12)]
        public string Telephone { get; set; }

        
    }
}
