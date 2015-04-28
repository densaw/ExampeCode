using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.SportsScience
{
    public class SportsScienceExerciseTableViewModel
    {
        public int Id { get; set; }
        public SportsScienceType Type { get; set; }
        public string Name { get; set; }
    }
}
