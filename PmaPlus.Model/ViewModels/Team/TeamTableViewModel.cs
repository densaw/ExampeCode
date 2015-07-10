using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Team
{
    public class TeamTableViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Progress { get; set; }

        public bool Archived { get; set; }
        public int CoachesCount { get; set; }

        public string CurriculumName { get; set; }

        public int PlayersCount { get; set; }
    }
}
