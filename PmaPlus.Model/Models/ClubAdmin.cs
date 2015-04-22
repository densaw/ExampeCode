using System.ComponentModel.DataAnnotations;

namespace PmaPlus.Model.Models
{
    public  class ClubAdmin
    {
      
        public int Id { get; set; }

        public virtual User User { get; set; }

        
        public virtual Club Club { get; set; }
    }
}
