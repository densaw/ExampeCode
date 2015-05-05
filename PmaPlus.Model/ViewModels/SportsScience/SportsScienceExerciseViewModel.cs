using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SportsScience
{
    public class SportsScienceExerciseViewModel
    {
        public int Id { get; set; }
        [Required]
        public SportsScienceExerciseType Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string Picture1 { get; set; }

        public string Picture2 { get; set; }

        public string Picture3 { get; set; }

        public string Video { get; set; }
    }
}
