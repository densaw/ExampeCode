using System;
using System.Globalization;

namespace PmaPlus.Data
{
    public class DateTool
    {
        
        public static int GetWeekNumber(DateTime date)
        {

            var dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;
            return calendar.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public static int GetThisWeek()
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;
            return calendar.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            
        }
    }
}