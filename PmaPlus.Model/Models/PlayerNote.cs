using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class PlayerNote
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public int Type { get; set; }
        public Session Session { get; set; }
        public string Note { get; set; }


        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
