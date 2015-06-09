using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class MessageComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int MessageId { get; set; }
        public DateTime SendAt { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Message Message { get; set; }
    }
}
