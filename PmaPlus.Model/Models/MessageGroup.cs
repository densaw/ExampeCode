using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class MessageGroup
    {
        public MessageGroup()
        {
            MessagePrivates = new List<MessagePrivate>();
        }

        public int MessageGroupId { get; set; }
        public string GroupName { get; set; }
        public virtual ICollection<MessagePrivate> MessagePrivates { get; set; }
        public virtual ICollection<User> Users { get; set; } 
    }
}
