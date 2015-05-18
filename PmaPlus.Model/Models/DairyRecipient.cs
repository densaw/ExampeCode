using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class DairyRecipient
    {
        public int Id { get; set; }
        public Diary Diary { get; set; }
        public User User { get; set; }
        public bool Accepted { get; set; }
    }
}
