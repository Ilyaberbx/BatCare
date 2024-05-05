using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Saves.Runtime.Data;
using Better.Services.Runtime;
using Workspace.Extensions;
using Workspace.Services.Persistence;
using Workspace.Services.Persistence.Data.Time;

namespace Workspace.Services.Time
{
    public class RealtimeService : MonoService
    {
        public DateTime Realtime => _startTime.AddSeconds(UnityEngine.Time.realtimeSinceStartup);
        private bool IsFirstSession => RealtimeProperty.Value.IsEmpty();
        private SavesProperty<TimeData> RealtimeProperty => _userService.RealLastSession;

        private UserService _userService;

        private DateTime _lastSessionEnd;

        private DateTime _startTime;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _userService = ServiceLocator.Get<UserService>();

            await LoadTime();
            
            if (!IsFirstSession)
            {
                _lastSessionEnd = RealtimeProperty.Value.ToDateTime();
            }
        }

        protected override async void OnDestroy()
        {
            base.OnDestroy();

            await LoadTime();

            RealtimeProperty.Value = _startTime.ToData();
        }

        public bool TryGetOfflineSpan(out TimeSpan offlineSpan)
        {
            offlineSpan = default;

            if (_lastSessionEnd == default)
            {
                return false;
            }

            offlineSpan = Realtime - _lastSessionEnd;

            return true;
        }

        // TODO: Internet check
        private Task LoadTime()
        {
            _startTime = DateTime.Now;

            return Task.CompletedTask;
        }
    }
}