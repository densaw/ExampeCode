using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.ViewModels.Player;

namespace PmaPlus.Model.ViewModels
{
    public class PlayersForScoutPage : Page
    {
        public IEnumerable<PlayerTableForScoutViewModel> Items { get; set; }
    }
}
