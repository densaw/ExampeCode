using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Team
{
    public class AddTeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Archived { get; set; }
        public int CurriculumId { get; set; }
        public List<int> Coaches { get; set; }
        public List<int> Players { get; set; } 
    }
}
