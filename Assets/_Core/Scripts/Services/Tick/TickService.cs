using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Workspace.Services.Pause;

namespace Workspace.Services.Tick
{
    public class TickService : MonoService
    {
        private readonly List<ITickable> _tickables = new List<ITickable>();
        private PauseService _pauseService;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            _pauseService = ServiceLocator.Get<PauseService>();
            
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
            if(_pauseService.IsPaused || !Initialized)
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