using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.Saves.Runtime.Data;
using Better.Services.Runtime;
using Workspace.Extensions;
using Workspace.Services.Persistence;
using Workspace.Services.Persistence.Data.Time;
using Workspace.Services.Tick;
using Workspace.Utilities;

namespace Workspace.Services.Time
{
    public class RealtimeService : PocoService, ITickable
    {
        private UserService _userService;

        private DateTime _lastSessionEnd;

        private DateTime _startTime;
        public bool TickOnPause => true;
        public DateTime Realtime => _startTime.AddSeconds(UnityEngine.Time.realtimeSinceStartup);
        private bool IsFirstSession => RealtimeProperty.Value.IsEmpty();
        private SavesProperty<TimeData> RealtimeProperty => _userService.RealLastSession;
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _userService = ServiceLocator.Get<UserService>();
            
            await LoadTime();
            
            UpdatesUtility.Subscribe(this);
            
            if (!IsFirstSession)
            {
                _lastSessionEnd = RealtimeProperty.Value.ToDateTime();
            }
        }

        public void Tick(float deltaTime)
        {
            RealtimeProperty.Value = Realtime.ToData();
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