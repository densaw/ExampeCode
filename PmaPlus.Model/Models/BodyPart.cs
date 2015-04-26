using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class BodyPart
    {
       
        public int Id { get; set; }

        public BodyPartType Type  { get; set; }

        public string InjuryName { get; set; }

        public string Description { get; set; }

        public string Picture { get; set; }

        public string Treatment { get; set; }

    }
}
