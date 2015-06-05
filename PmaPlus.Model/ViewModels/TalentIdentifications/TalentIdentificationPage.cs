using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.ViewModels.TalentIdentifications;
using PmaPlus.Model.ViewModels.Team;

namespace PmaPlus.Model.ViewModels.Skill
{
    public class TalentIdentificationPage : Page
    {
        public IEnumerable<TalentIdentificationViewModel> Items { get; set; }
    }
}
