using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PmaPlus.Model.Models
{
    public  class ClubAdmin
    {
      
        public int Id { get; set; }

        public virtual User User { get; set; }

        
        public virtual Club Club { get; set; }
    }
}
