using Better.Locators.Runtime;
using Workspace.Services.Pause;

namespace Workspace.Utilities
{
    public static class PauseUtility
    {
        private static readonly ServiceProperty<PauseService> _pauseServiceProperty = new();
        private static PauseService PauseService => _pauseServiceProperty.CachedService;

        public static bool IsPaused() => PauseService.IsPaused;
        public static void Subscribe(IPauseListener listener) => PauseService.Subscribe(listener);

        public static void Unsubscribe(IPauseListener listener) => PauseService.Unsubscribe(listener);
    }
}