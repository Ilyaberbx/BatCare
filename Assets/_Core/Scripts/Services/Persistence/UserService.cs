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
        public SavesProperty<TimeData> PreviousSessionEnd { get; private set; }
        public SavesProperty<InGameSettingsData> InGameSettingsProperty { get; private set; }
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            PreviousSessionEnd = new(nameof(TimeData), new TimeData());
            InGameSettingsProperty = new(nameof(InGameSettingsData), Settings.InGameInGameSettings);
            return Task.CompletedTask;
        }
    }
}