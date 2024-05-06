using System;
using Better.Locators.Runtime;
using UnityEngine;
using Workspace.Extensions;
using Workspace.Services.Time;

namespace Workspace.Utilities
{
    public static class SunUtility
    {
        private static readonly ServiceProperty<TimeOfDayService> _timeOfDayServiceProperty = new();

        private static TimeOfDayService TimeOfDayService => _timeOfDayServiceProperty.CachedService;

        private static TimeSpan SunriseTime => TimeOfDayService.SunriseSpan;
        private static TimeSpan SunsetTime => TimeOfDayService.SunsetSpan;
        private static TimeSpan CurrentTime => TimeOfDayService.GameTime.TimeOfDay;
        
        public static float GetSunAngle()
        {
            var isDay = TimeOfDayService.IsDay();

            var startDegree = isDay ? 0f : 180f;

            var startTime = isDay ? SunriseTime : SunsetTime;
            var endTime = isDay ? SunsetTime : SunriseTime;

            var totalTime = startTime.Difference(endTime);
            var elapsedTime = startTime.Difference(CurrentTime);

            var percentage = (float)(elapsedTime.TotalMinutes / totalTime.TotalMinutes);
            
            return Mathf.Lerp(startDegree, startDegree + 180f, percentage);
        }
    }
}