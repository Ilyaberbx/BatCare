using Workspace.Core.Systems;

namespace Workspace.Services.Pause
{
    public interface IPauseSystem : ISystem
    {
        bool IsPaused { get; }
        void Subscribe(IPauseListener listener);
        void Unsubscribe(IPauseListener listener);
        void Pause(bool value);
    }
}