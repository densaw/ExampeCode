namespace PmaPlus.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class SportsScienceTest
    {
        public int Id { get; set; }

        public int? TestType { get; set; }

        public string TestName { get; set; }

        public string TestDescription { get; set; }

        public string ZScoreFormula { get; set; }

        public string Measure { get; set; }

        public string LowValue { get; set; }

        public string HightValue { get; set; }

        public string NationalAverage { get; set; }

        public string TestVideo { get; set; }
    }
}
