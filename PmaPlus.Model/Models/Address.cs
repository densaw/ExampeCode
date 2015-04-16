using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public  class Address
    {
    

       
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string Address3 { get; set; }

        [StringLength(12)]
        public string Telephone { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        
        [StringLength(50)]
        public string TownCity { get; set; }

        public int? PostCode { get; set; }

       
    }
}
