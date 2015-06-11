using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PmaPlus.Model.Enums;

namespace PmaPlus.Model.Models
{
    public class SessionAttendance
    {
        public int Id { get; set; }

        public AttendanceType Attendance { get; set; }
        public int Duration { get; set; }

        public int PlayerId  { get; set; }
        public virtual Player Player { get;set; }


        public int SessionResultId { get; set; }
        public virtual SessionResult SessionResult { get; set; }

    }
}
