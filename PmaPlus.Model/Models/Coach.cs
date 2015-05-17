using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class Coach
    {
        public int Id { get; set; }

        

        public int? TeamId { get; set; }

        public virtual Club Club { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public UserStatus Status { get; set; }
        public virtual User User { get; set; }
    }
}
