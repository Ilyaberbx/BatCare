using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using UnityEngine;

namespace Workspace.Services.Time
{
    public class TimeService : MonoService
    {
        public event Action OnSunrise;

        public event Action OnSunset;

        public event Action OnHourChange;
        
        [SerializeField, Min(0.1f)] private float _timeMultiplier;
        [SerializeField] private float _sunriseHour;
        [SerializeField] private float _sunsetHour;
        
        private SessionsService _sessionsService;

        private DateTime _currentTime;

        private TimeSpan _sunriseSpan;

        private TimeSpan _sunsetSpan;

        private ReactiveProperty<bool> IsDayProperty { get; set; } = new();

        private ReactiveProperty<int> CurrentHourProperty { get; set; } = new();


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _sessionsService = ServiceLocator.Get<SessionsService>();

            CalculateStartTime();

            IsDayProperty.Subscribe(day => (day ? OnSunrise : OnSunset)?.Invoke());
            CurrentHourProperty.Subscribe(_ => OnHourChange?.Invoke());

            return Task.CompletedTask;
        }

        private void Update() => Tick(UnityEngine.Time.deltaTime);

        private void Tick(float deltaTime)
        {
            _currentTime = _currentTime.AddSeconds(deltaTime * _timeMultiplier);
            IsDayProperty.Value = IsDayTime();
            CurrentHourProperty.Value = _currentTime.Hour;
        }

        private void CalculateStartTime()
        {
            var currentRealtime = _sessionsService.GetCurrentTime();

            if (_sessionsService.TryGetPreviousEnd(out var previousSessionEnd))
            {
                var offlineSpan = previousSessionEnd - currentRealtime;

                _currentTime = _currentTime.Add(offlineSpan);
            }
            else
            {
                _currentTime = currentRealtime;
            }

            _sunriseSpan = TimeSpan.FromHours(_sunriseHour);

            _sunsetSpan = TimeSpan.FromHours(_sunsetHour);
        }

        private bool IsDayTime() => _currentTime.TimeOfDay > _sunriseSpan && _currentTime.TimeOfDay < _sunsetSpan;
    }
}