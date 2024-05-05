using System;
using System.Text.RegularExpressions;
using Workspace.Services.Persistence.Data.Time;

namespace Workspace.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this WorldTimeData worldTimeData)
        {
            var dateTime = worldTimeData.DateTime;

            var datePattern = @"^\d{4}-\d{2}-\d{2}";
            var timePattern = @"\d{2}:\d{2}:\d{2}";

            var date = Regex.Match(dateTime, datePattern);
            var time = Regex.Match(dateTime, timePattern);

            return DateTime.Parse($"{date} {time}");
        }

        public static TimeData ToData(this DateTime dateTime)
        {
            return new TimeData()
            {
                Value = dateTime.ToString()
            };
        }
        
        public static DateTime ToDateTime(this TimeData data)
        {
            return DateTime.Parse(data.Value);
        }
    }
}