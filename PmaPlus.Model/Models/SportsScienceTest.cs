using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class SportsScienceTest
    {
        public int Id { get; set; }

        public SportsScienceType Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ZScoreFormulaType ZScoreFormula { get; set; }

        public string Measure { get; set; }

        public string LowValue { get; set; }

        public string HightValue { get; set; }

        public string NationalAverage { get; set; }

        public string Video { get; set; }
    }
}
