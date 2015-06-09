using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.MessageWall
{
    public class MessageCommentViewModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime SendAt { get; set; }
        public string UserName { get; set; }
        public string UserAva { get; set; }
        public int UserId { get; set; }
    }
}
