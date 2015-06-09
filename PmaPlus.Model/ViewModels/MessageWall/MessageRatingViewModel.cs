using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.MessageWall
{
    public class MessageRatingViewModel
    {
        public string UserName { get; set; }
        public string UserAva { get; set; }
        public bool Rating { get; set; }
        public int UserId { get; set; }
    }
}
