using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Models
{
    public class Message
    {
        public Message()
        {
            Comments = new List<MessageComment>();
            Ratings = new List<MessageRating>();
        }

        public int MessageId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime SendAt { get; set; }
        public int UserId { get; set; }
        public int ClubId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MessageComment> Comments { get; set; }
        public virtual ICollection<MessageRating> Ratings { get; set; } 
    }
}
