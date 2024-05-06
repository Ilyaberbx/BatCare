using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Services.Runtime;
using Workspace.Utilities;

namespace Workspace.Services.Tick
{
    public class TickService : MonoService
    {
        private readonly List<ITickable> _tickables = new List<ITickable>();
    
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Subscribe(ITickable tickable)
        {
            _tickables.Add(tickable);
        }

        public void Unsubscribe(ITickable tickable)
        {
            if (_tickables.IsEmpty())
                return;

            if (_tickables.Contains(tickable))
            {
                _tickables.Remove(tickable);
            }
        }

        private void Update()
        {
            if (PauseUtility.IsPaused())
                return;
            
            if (_tickables.IsEmpty())
                return;

            foreach (var tickable in _tickables)
            {
                tickable.Tick(UnityEngine.Time.deltaTime);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _tickables.Clear();
        }
    }
}