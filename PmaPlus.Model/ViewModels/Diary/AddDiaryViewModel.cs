using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Diary
{
    public class AddDiaryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public string Url { get; set; }
        public IList<Role> AttendeeTypes { get; set; }
        public IList<int> SpecificPersons { get; set; }
    }
}
