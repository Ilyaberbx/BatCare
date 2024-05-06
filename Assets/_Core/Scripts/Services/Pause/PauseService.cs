using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime;

namespace Workspace.Services.Pause
{
    public class PauseService : PocoService
    {
        private List<IPauseListener> _pauseListeners;
        public bool IsPaused { get; private set; }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _pauseListeners = new();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Subscribe(IPauseListener listener)
        {
            _pauseListeners.Add(listener);
        }

        public void Unsubscribe(IPauseListener listener)
        {
            if (_pauseListeners.IsEmpty())
                return;

            if (_pauseListeners.Contains(listener))
            {
                _pauseListeners.Remove(listener);
            }
        }
        
        public void Pause(bool value)
        {
            IsPaused = value;

            foreach (var listener in _pauseListeners)
            {
                listener.OnPaused(value);
            }
        }
    }
}