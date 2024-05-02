using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Workspace.Extensions;
using Workspace.Installers.Services;
using Workspace.Networking.Time;
using Workspace.Services.EventBus;
using Workspace.Services.EventBus.Handlers;
using Workspace.Services.Persistence;
using Workspace.Utilities;

namespace Workspace.Services.Time
{
    public class SessionsService : DisposablePocoService
        , IApplicationCloseSubscriber
    {
        public int Priority => 0;

        private DateTime _sessionEntryTime;
        private UserService _userService;
        private EventBusService _eventBusService;

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                _sessionEntryTime = await WorldTimeApi.GetTimeByIp();
            }
            catch
            {
                _sessionEntryTime = DateTime.Now;
            }
        }

        protected override async Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _userService = await ServiceLocatorUtility.WaitForService<UserService>(cancellationToken);
            _eventBusService = await ServiceLocatorUtility.WaitForService<EventBusService>(cancellationToken);

            _eventBusService.Subscribe(this);
            
            Debug.Log("Current session entry time: " + _sessionEntryTime);
        }

        public override void Dispose()
        {
            _eventBusService.Unsubscribe(this);
        }

        public void OnApplicationClose()
        {
            Debug.Log("Current session end time: " + _sessionEntryTime);
            
            _userService.PreviousSessionEnd.Value = GetCurrentTime().ToData();
        }

        public bool TryGetPreviousEnd(out DateTime dateTime)
        {
            dateTime = default;

            var time = _userService.PreviousSessionEnd.Value.Value;
            
            return !string.IsNullOrEmpty(time) && DateTime.TryParse(time, out dateTime);
        }

        public DateTime GetCurrentEntry()
        {
            return _sessionEntryTime;
        }

        public DateTime GetCurrentTime()
        {
            return _sessionEntryTime.AddSeconds(UnityEngine.Time.realtimeSinceStartup);
        }
    }
}