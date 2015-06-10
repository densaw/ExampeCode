using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class MessagePrivate
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime SendAt { get; set; }
        public int UserId { get; set; }
        public int MessageGroupId { get; set; }
        public virtual User User { get; set; }
        public virtual MessageGroup MessageGroup { get; set; }
    }
}
