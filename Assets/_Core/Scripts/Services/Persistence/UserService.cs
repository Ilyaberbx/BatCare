using System.Threading;
using System.Threading.Tasks;
using Better.Saves.Runtime.Data;
using Better.Services.Runtime;
using Workspace.Services.Persistence.Data;

namespace Workspace.Services.Persistence
{
    public class UserService : PocoService<UserServiceSettings>
    {
        private const string RealtimeLastSessionKey = "Real Time Last Session";
        private const string GametimeLastSessionKey = "Game Time Last Session";
        private const string CurrentLocationKey = "Current Locatio nData";
        public SavesProperty<TimeData> RealLastSession { get; private set; }
        public SavesProperty<TimeData> GameLastSession { get; private set; }
        public SavesProperty<SettingsData> SettingsProperty { get; private set; }
        public SavesProperty<int> CurrentRoomIndexProperty { get; private set; }

        public SavesProperty<WallpapersData> WallpapersProperty { get; private set; }
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            RealLastSession = new(RealtimeLastSessionKey, new TimeData());
            GameLastSession = new(GametimeLastSessionKey, new TimeData());
            CurrentRoomIndexProperty = new(CurrentLocationKey, 0);
            SettingsProperty = new(nameof(SettingsData), Settings.Settings);

            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}