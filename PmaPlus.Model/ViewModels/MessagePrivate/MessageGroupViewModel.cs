using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.MessagePrivate
{
    public class MessageGroupViewModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public IQueryable<UsersList> Users { get; set; }
        public IQueryable<MessagePrivateViewModel> Messages { get; set; } 
    }
}
