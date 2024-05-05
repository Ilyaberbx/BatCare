using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using UnityEngine;
using Workspace.Extensions;
using Workspace.Services.Persistence;

namespace Workspace.Services.Time
{
    public class GameTimeService : MonoService
    {
        private bool IsFirstSession => _userService.GameLastSession.Value.IsEmpty();

        [SerializeField, Min(0.1f)] private float _timeMultiplier;
        [SerializeField] private float _sunriseHour;
        [SerializeField] private float _sunsetHour;

        private RealtimeService _realtimeService;
        private UserService _userService;
        
        public ReactiveProperty<DateTime> TimeProperty;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            TimeProperty = new();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _realtimeService = ServiceLocator.Get<RealtimeService>();
            _userService = ServiceLocator.Get<UserService>();
            
            TimeProperty.Value = LoadTime();
            
            return Task.CompletedTask;
        }

        private void Update()
        {
            TimeProperty.Value = TimeProperty.Value.AddSeconds(UnityEngine.Time.deltaTime * _timeMultiplier);
            
            _userService.GameLastSession.Value = TimeProperty.Value.ToData();
        }
        

        private DateTime LoadTime()
        {
            if (IsFirstSession)
            {
                return _realtimeService.Realtime;
            }

            var lastSession = _userService.GameLastSession.Value.ToDateTime();

            if (_realtimeService.TryGetOfflineSpan(out var offlineSpan))
            {
                return lastSession.AddSeconds(offlineSpan.TotalSeconds * _timeMultiplier);
            }

            return lastSession;
        }
    }
}