using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PasswordHistory
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Password { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeAt { get; set; }
        public string Ip { get; set; }
    }
}
