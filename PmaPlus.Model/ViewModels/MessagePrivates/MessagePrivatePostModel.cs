using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.MessagePrivates
{
    public class MessagePrivatePostModel
    {
        public MessagePrivateViewModel MessagePrivate { get; set; }
        public IList<int> UsersInGroup { get; set; } 
    }
}
