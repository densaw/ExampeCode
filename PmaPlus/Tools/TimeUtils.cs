using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PmaPlus.Tools
{
    public class TimeUtils
    {
        public static string GetGreetingTime()
        {
            var hour = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).Hour;
            if (hour > 0 && hour < 12)
            {
                return "morning";
            }
            else if(hour >= 12 && hour <= 17)
            {
                return "afternoon";
            }
            else if (hour > 17 && hour < 24)
            {
                return "evening";
            }
            return "NaN";
        }
    }
}