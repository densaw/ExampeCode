using System;
using System.Collections.Generic;
using System.Drawing;
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


        public static Size GetThumbnailSize(Image original,int maxSize)
        {
            // Maximum size of any dimension.
            int maxPixels = maxSize;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = (double)maxPixels / originalWidth;
            }
            else
            {
                factor = (double)maxPixels / originalHeight;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
    }
}