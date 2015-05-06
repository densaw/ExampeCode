using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.PlayerAttribute
{
    public class PlayerAttributePage : Page
    {
        public IEnumerable<PlayerAttributeTableViewModel> Items { get; set; }
    }
}
