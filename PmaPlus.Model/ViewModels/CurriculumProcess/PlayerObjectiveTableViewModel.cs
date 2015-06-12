using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class PlayerObjectiveTableViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public string Objective { get; set; }
        public string Outcome { get; set; }
        public string FeedBack { get; set; }
    }
}
