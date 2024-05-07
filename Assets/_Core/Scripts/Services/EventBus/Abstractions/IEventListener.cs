namespace Workspace.Services.EventBus.Abstractions
{
    public interface IEventListener
    {
    }

    public interface IPriorityEventListener
    {
    }

    public interface IEventListener<in TEvent> : IEventListener where TEvent : struct, IEvent
    {
        void OnEvent(TEvent eventData);
    }

    public interface IPriorityEventListener<in TEvent> : IPriorityEventListener, IEventListener<TEvent>
        where TEvent : struct, IEvent
    {
        int Priority { get; }
    }
}