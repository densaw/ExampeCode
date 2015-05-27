using System.Collections.Generic;

namespace PmaPlus.Model.Models
{
    public class Curriculum
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AgeGroupType AgeGroup { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }

        public virtual Club Club { get; set; }
    }
}
