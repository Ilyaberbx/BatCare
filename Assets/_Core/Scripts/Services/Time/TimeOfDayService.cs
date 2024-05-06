using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Services.Runtime;

namespace Workspace.Services.Time
{
    public class TimeOfDayService : PocoService<TimeOfDaySettings>
    {
        private GameTimeService _gameTimeService;
        private TimeSpan _sunsetSpan;
        private TimeSpan _sunriseSpan;

        public DateTime GameTime => _gameTimeService.TimeProperty.Value;

        public TimeSpan SunsetSpan => _sunsetSpan;

        public TimeSpan SunriseSpan => _sunriseSpan;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _sunsetSpan = TimeSpan.FromHours(Settings.SunsetHour);
            _sunriseSpan = TimeSpan.FromHours(Settings.SunriseHour);
            
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _gameTimeService = ServiceLocator.Get<GameTimeService>();
            
            return Task.CompletedTask;
        }

        public bool IsDay() => GameTime.TimeOfDay < SunsetSpan && GameTime.TimeOfDay > SunriseSpan;
    }
}