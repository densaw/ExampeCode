using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.ViewModels.Team;

namespace PmaPlus.Model.ViewModels.Skill
{
    public class TeamsForPhisiotherapistPage : Page
    {
        public IEnumerable<TeamTableForPhisiotherapistViewModel> Items { get; set; }
    }
}
