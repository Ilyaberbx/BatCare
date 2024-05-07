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

        public DateTime GameTime => _gameTimeService.TimeProperty.Value;

        public TimeSpan SunsetSpan { get; private set; }

        public TimeSpan SunriseSpan { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            SunsetSpan = TimeSpan.FromHours(Settings.SunsetHour);
            SunriseSpan = TimeSpan.FromHours(Settings.SunriseHour);
            
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