using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Skill
{
    public class SkillVideoPage : Page
    {
        public IQueryable<SkillVideoTableViewModel> Items { get; set; }
    }
}
