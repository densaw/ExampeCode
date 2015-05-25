using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumDetailViewModel
    {
        public int CurriculumDetailId { get; set; }

        public string CurriculumDetailName { get; set; }

        public string CurriculumDetailNumber { get; set; }

        public string CurriculumDetailCoachPicture { get; set; }
        public string CurriculumDetailCoachDescription { get; set; }

        public string CurriculumDetailPlayersFriendlyName { get; set; }

        public string CurriculumDetailPlayersFriendlyPicture { get; set; }

        public string CurriculumDetailPlayersDescription { get; set; }

        public int ScenarioId { get; set; }

        public int Id { get; set; }
    }
}
