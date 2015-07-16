using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class SessionAttendanceDetailViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}
