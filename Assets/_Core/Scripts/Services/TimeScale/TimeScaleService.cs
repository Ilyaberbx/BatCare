using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.DataStructures.Properties;
using Better.Services.Runtime;
using Workspace.Services.Pause;
using Workspace.Utilities;

namespace Workspace.Services.TimeScale
{
    // TODO: Remake subscribers using event bus
    public class TimeScaleService : PocoService, IPauseListener
    {
        private ReactiveProperty<float> _timeScaleProperty;
        public float TimeScale => _timeScaleProperty.Value;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _timeScaleProperty = new();
            return Task.CompletedTask;;
        }
        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            PauseUtility.Subscribe(this);
            return Task.CompletedTask;
        }
        public void Subscribe(Action<float> action) => _timeScaleProperty.Subscribe(action);
        public void Unsubscribe(Action<float> action) => _timeScaleProperty.Unsubscribe(action);
        public void OnPaused(bool isPause) => _timeScaleProperty.Value = isPause ? 0f : 1f;
    }
}