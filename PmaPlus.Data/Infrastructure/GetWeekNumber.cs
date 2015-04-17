using System;
using System.Globalization;

namespace PmaPlus.Data
{
    public class DateTool
    {
        private DateTime _date;
        public static int GetWeekNumber(DateTime? date)
        {
            
            if (!date.HasValue)
                return 0;

            
           

            var dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;
            return calendar.GetWeekOfYear(date.Value, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public static int GetThisWeek()
        {
            var dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = dfi.Calendar;
            return calendar.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            
        }
    }
}