using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class DiaryRecipient
    {
        public int Id { get; set; }
        public virtual Diary Diary { get; set; }
        public virtual User Owner { get; set; }
        public virtual User Recipient { get; set; }
        public bool Accepted { get; set; }
    }
}
