namespace Workspace.Services.EventBus.Abstractions
{
    public interface IEventListener
    {
    }

    public interface IEventListener<in TEvent> : IEventListener where TEvent : struct, IEvent
    {
        void OnEvent(TEvent eventData);
    }

    public interface IPriorityEventListener<in TEvent> : IEventListener<TEvent> where TEvent : struct, IEvent
    {
        int Priority { get; }
    }
}