using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Workspace.Services.EventBus.Abstractions;

namespace Workspace.Services.EventBus
{
    // TODO: Remake using Callback With Priority
    public class EventBusService : PocoService, IEventBus
    {
        private IEventBus _internalEventBus;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _internalEventBus = new InternalEventBus();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #region EventBus

        public void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : struct, IEvent => _internalEventBus.Subscribe(listener);

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : struct, IEvent => _internalEventBus.Unsubscribe(listener);

        public void Raise<TEvent>(TEvent eventData) where TEvent : struct, IEvent => _internalEventBus.Raise(eventData);

        #endregion
    }
}