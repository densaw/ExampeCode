using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.ViewModels.MessageWall;

namespace PmaPlus.Model
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime SendAt { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserAva { get; set; }
        public IList<MessageCommentViewModel> Comments { get; set; }
        public IList<MessageRatingViewModel> RatingPositive { get; set; }
        public IList<MessageRatingViewModel> RatingNegative { get; set; }
    }
}
