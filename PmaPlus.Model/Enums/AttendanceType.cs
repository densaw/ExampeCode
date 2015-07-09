using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PmaPlus.Model.Enums
{
    public enum AttendanceType
    {
        Attended = 0,
        NotAttended = 1,
        Holidays = 2,
        Injured = 3,
        School = 4,
        Sick = 5,
        Other = 6,
        Training = 7,
        Undefined = -1
    }
}
