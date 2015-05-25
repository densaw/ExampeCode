using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public  class CurriculumDetail
    {
       
        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string CoachPicture { get; set; }
      
        public string CoachDescription { get; set; }

        public string PlayersFriendlyName { get; set; }

        public string PlayersFriendlyPicture { get; set; }

        public string PlayersDescription { get; set; }

        public virtual Scenario Scenario { get; set; }

    }
}
