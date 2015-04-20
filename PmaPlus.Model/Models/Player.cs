using System.Collections.Generic;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class Player
    {
        public int Id { get; set; }
        public virtual Club Club { get; set; }
        public virtual Team Team { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual User User { get; set; }
        public PlayerStatus Status { get; set; }
        public virtual ICollection<PlayerInjury> PlayerInjuries { get; set; }
    }
}
