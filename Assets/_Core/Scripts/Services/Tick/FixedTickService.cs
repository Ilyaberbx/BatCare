using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Services.Runtime;
using Workspace.Services.Pause;

namespace Workspace.Services.Tick
{
    public class FixedTickService : MonoService
    {
        private readonly List<IFixedTickable> _fixedTickables = new List<IFixedTickable>();
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

        public void Subscribe(IFixedTickable tickable)
        {
            _fixedTickables.Add(tickable);
        }

        public void Unsubscribe(IFixedTickable tickable)
        {
            if (_fixedTickables.IsEmpty())
                return;

            if (_fixedTickables.Contains(tickable))
            {
                _fixedTickables.Remove(tickable);
            }
        }

        private void FixedUpdate()
        {
            if (_pauseService.IsPaused || !Initialized)
                return;
            
            if (_fixedTickables.IsEmpty())
                return;

            foreach (var tickable in _fixedTickables)
            {
                tickable.FixedTick(UnityEngine.Time.fixedDeltaTime);
            }
        }
    }
}