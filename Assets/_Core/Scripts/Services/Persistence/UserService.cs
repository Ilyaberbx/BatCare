using System.Threading;
using System.Threading.Tasks;
using Better.Saves.Runtime.Data;
using Better.Services.Runtime;
using Workspace.Services.Persistence.Data.Settings;
using Workspace.Services.Persistence.Data.Time;

namespace Workspace.Services.Persistence
{
    public class UserService : PocoService<UserServiceSettings>
    {
        public SavesProperty<TimeData> RealLastSession { get; private set; }
        public SavesProperty<TimeData> GameLastSession { get; private set; }
        public SavesProperty<InGameSettingsData> GameSettingsProperty { get; private set; }
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            RealLastSession = new("RealTimeLastSession", new TimeData());
            GameLastSession = new("GameTimeLastSession", new TimeData());
            GameSettingsProperty = new(nameof(InGameSettingsData), Settings.InGameInGameSettings);
            
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}