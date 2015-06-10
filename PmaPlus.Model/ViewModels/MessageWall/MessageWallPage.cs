using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.MessageWall
{
    public class MessageWallPage : Page
    {
        public IEnumerable<MessageViewModel> Items { get; set; } 
    }
}
