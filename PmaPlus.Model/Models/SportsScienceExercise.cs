namespace PmaPlus.Model.Models
{
    public class SportsScienceExercise
    {
        public int Id { get; set; }

        public SportsScienceTestType Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

       
        public string Measure { get; set; }

        public string LowValue { get; set; }

        public string HightValue { get; set; }

        public string NationalAverage { get; set; }

        public string Video { get; set; }
    }
}
