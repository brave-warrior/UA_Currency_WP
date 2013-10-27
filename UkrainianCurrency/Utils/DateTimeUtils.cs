using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UkrainianCurrency.Utils
{
    public class DateTimeUtils
    {
        /// <summary>
        /// Converts date in ticks to the string in local time
        /// </summary>
        /// <param name="aTimeInTicks">date in ticks</param>
        /// <returns></returns>
        public static string GetDateTime(long aTimeInTicks)
        {
            DateTime dateTime = new DateTime(aTimeInTicks);
            dateTime = dateTime.ToLocalTime();

            string dateTimeStr = dateTime.ToString("dd-MMM-yyyy  HH:mm");
            return dateTimeStr;
        }

       
    }
}
