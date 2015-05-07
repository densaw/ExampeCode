using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public  class PlayerAttribute
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public AttributeType Type { get; set; }

        public int? MaxScore { get; set; }

        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }
    }
}
