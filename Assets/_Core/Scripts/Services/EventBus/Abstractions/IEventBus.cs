namespace Workspace.Services.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : struct, IEvent;
        void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : struct, IEvent;
        void Raise<TEvent>(TEvent eventData) where TEvent : struct, IEvent;
    }
}