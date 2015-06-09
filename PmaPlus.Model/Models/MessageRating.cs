using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class MessageRating
    {
        public int Id { get; set; }
        public int MessagesId { get; set; }
        public int UserId { get; set; }
        public bool Rating { get; set; }
        public virtual Message Messages { get; set; }
        public virtual User User { get; set; }
    }
}
