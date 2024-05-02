using Better.Locators.Runtime;
using Workspace.Services.UI.Implementations;

namespace Workspace.Utilities
{
    public static class UIFacadeUtility
    {
        private static readonly ServiceProperty<ModalService> _modalProperty = new();
        private static readonly ServiceProperty<ScreenService> _screenProperty = new();

        private static ModalService Modals => _modalProperty.CachedService;
        private static ScreenService Screens => _screenProperty.CachedService;

        public static void Clear()
        {
            if (_modalProperty.IsRegistered)
                Modals.Close();

            if (_screenProperty.IsRegistered)
                Screens.Close();
        }
    }
}