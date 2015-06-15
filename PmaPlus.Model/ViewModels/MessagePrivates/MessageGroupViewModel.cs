using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Models;

namespace PmaPlus.Model.ViewModels.MessagePrivates
{
    public class MessageGroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public IList<UsersList> Users { get; set; }
        public IList<MessagePrivateViewModel> Messages { get; set; } 
    }
}
