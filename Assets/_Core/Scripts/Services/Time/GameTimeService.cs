using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using UnityEngine;
using Workspace.Extensions;
using Workspace.Services.Persistence;
using Workspace.Services.Tick;

namespace Workspace.Services.Time
{
    [Serializable]
    public class GameTimeService : PocoService, ITickable
    {
        [SerializeField, Min(0.1f)] private float _timeMultiplier;

        private RealtimeService _realtimeService;
        private UserService _userService;

        public ReactiveProperty<DateTime> TimeProperty { get; private set; }
        private bool IsFirstSession => _userService.GameLastSession.Value.IsEmpty();
        public bool TickOnPause => true;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            TimeProperty = new();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _realtimeService = ServiceLocator.Get<RealtimeService>();
            _userService = ServiceLocator.Get<UserService>();
            
            ServiceLocator.Get<TickService>().Subscribe(this);
            
            TimeProperty.Value = LoadTime();
            
            return Task.CompletedTask;
        }

        public void Tick(float deltaTime)
        {
            TimeProperty.Value = TimeProperty.Value.AddSeconds(deltaTime * _timeMultiplier);
            
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