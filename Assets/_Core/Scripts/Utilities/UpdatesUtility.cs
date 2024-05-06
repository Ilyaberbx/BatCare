using Better.Locators.Runtime;
using Workspace.Services.Tick;

namespace Workspace.Utilities
{
    public static class UpdatesUtility
    {
        private static readonly ServiceProperty<FixedTickService> _fixedTickProperty = new();
        private static readonly ServiceProperty<TickService> _tickProperty = new();
        private static TickService TickService => _tickProperty.CachedService;
        private static FixedTickService FixedTickService => _fixedTickProperty.CachedService;

        public static void Subscribe(IFixedTickable fixedTickable)
        {
            FixedTickService.Subscribe(fixedTickable);
        }
        
        public static void Unsubscribe(IFixedTickable fixedTickable)
        {
            FixedTickService.Unsubscribe(fixedTickable);
        }
        
        public static void Subscribe(ITickable tickable)
        {
            TickService.Subscribe(tickable);
        }
        
        public static void Unsubscribe(ITickable tickable)
        {
            TickService.Unsubscribe(tickable);
        }
    }
}