using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class PhysiotherapyExercise
    {
        public int Id { get; set; }

        public PhysioExerciseType Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Videolink { get; set; }

        public string Picture { get; set; }
    }
}
