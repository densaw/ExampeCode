using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public  class Address
    {
       
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string TownCity { get; set; }
        public string PostCode { get; set; }
    }
}
