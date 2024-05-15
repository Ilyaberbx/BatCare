using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;

namespace Workspace.Services.Pause
{
    public class PauseService : PocoService, IPauseSystem
    {
        private PauseSystem _pauseSystem;

        public bool IsPaused => _pauseSystem.IsPaused;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _pauseSystem = new PauseSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        #region IPauseSystem

        public void Subscribe(IPauseListener listener) => _pauseSystem.Subscribe(listener);

        public void Unsubscribe(IPauseListener listener) => _pauseSystem.Unsubscribe(listener);

        public void Pause(bool value) => _pauseSystem.Pause(value);

        #endregion
    }
}