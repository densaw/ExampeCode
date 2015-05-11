using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class Scout
    {
        public int Id { get; set; }

        public virtual User User { get; set; }
        public UserStatus Status { get; set; }
        public Club Club { get; set; }
    }
}
