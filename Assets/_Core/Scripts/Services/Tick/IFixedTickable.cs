namespace Workspace.Services.Tick
{
    public interface IFixedTickable
    {
        void FixedTick(float fixedDeltaTime);
    }
}