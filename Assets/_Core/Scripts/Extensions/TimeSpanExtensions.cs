using System;

namespace Workspace.Extensions
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Difference(this TimeSpan from, TimeSpan to)
        {
            var difference = to - from;
            return difference.TotalHours < 0 ? difference + TimeSpan.FromHours(24) : difference;
        }
    }
}