using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Curriculum
{
    public class CurriculumDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string CoachDescription { get; set; }

        public string PlayersFriendlyName { get; set; }

        public string PlayersFriendlyPicture { get; set; }

        public string PlayersDescription { get; set; }
    }
}
