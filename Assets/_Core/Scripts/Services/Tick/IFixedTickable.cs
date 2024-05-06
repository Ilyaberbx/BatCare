namespace Workspace.Services.Tick
{
    //TODO: Add tick On Pause impementation
    public interface IFixedTickable
    {
        bool TickOnPause { get; }
        void FixedTick(float fixedDeltaTime);
    }
}