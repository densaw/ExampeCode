using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.Diary
{
    public class AddDiaryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }
        public string Url { get; set; }
        public IList<Role> AttendeeTypes { get; set; }
        public IList<int> SpecificPersons { get; set; }
    }
}
