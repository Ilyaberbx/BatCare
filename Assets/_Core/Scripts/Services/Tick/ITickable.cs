namespace Workspace.Services.Tick
{
    public interface ITickable
    {
        bool TickOnPause { get; }
        void Tick(float deltaTime);
    }
}