using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.ViewModels.CurriculumProcess
{
    public class SessionAttendanceTableViewModel
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Picture { get;set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public AttendanceType Attendance { get; set; }
        public decimal AttPercent { get; set; }
        public int WbPercent { get; set; }
        public decimal Cur { get; set; }
    }
}
