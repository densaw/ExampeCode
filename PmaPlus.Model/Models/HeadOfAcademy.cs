using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class HeadOfAcademy
    {
        public int Id { get; set; }

        public User User { get; set; }
        public UserStatus Status { get; set; }
        public Club Club { get; set; }
    }
}
