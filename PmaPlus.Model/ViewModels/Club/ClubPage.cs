using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Club
{
    public class ClubPage : Page
    {
        public IEnumerable<ClubTableViewModel> Items { get; set; }
    }
}
