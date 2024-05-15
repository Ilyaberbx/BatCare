using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.SerializedTypes;
using Better.Saves.Runtime.Data;
using Better.Services.Runtime;
using Workspace.Services.Persistence.Data.Settings;
using Workspace.Services.Persistence.Data.Time;

namespace Workspace.Services.Persistence
{
    public class UserService : PocoService<UserServiceSettings>
    {
        private const string RealtimeLastSessionKey = "RealTimeLastSession";
        private const string GametimeLastSessionKey = "GameTimeLastSession";
        private const string WallpapersKey = "WallpapersData";
        public SavesProperty<TimeData> RealLastSession { get; private set; }
        public SavesProperty<TimeData> GameLastSession { get; private set; }
        public SavesProperty<InGameSettingsData> GameSettingsProperty { get; private set; }
        public SavesProperty<SerializedDictionary<int, int>> WallpapersMapProperty { get; private set; }
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            RealLastSession = new(RealtimeLastSessionKey, new TimeData());
            GameLastSession = new(GametimeLastSessionKey, new TimeData());
            GameSettingsProperty = new(nameof(InGameSettingsData), Settings.InGameInGameSettings);
            WallpapersMapProperty = new(WallpapersKey, new SerializedDictionary<int, int>());
            
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}