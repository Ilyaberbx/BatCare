using Better.Locators.Runtime;
using UnityEngine;
using Workspace.Services.EventBus;
using Workspace.Services.EventBus.Handlers;

namespace Workspace.Utilities.Application
{
    public class ApplicationCloseHandler : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            var eventBus = ServiceLocator.Get<EventBusService>();
            
            eventBus.RaiseEvent<IApplicationCloseSubscriber>
                (subscriber => subscriber.OnApplicationClose());
        }
    }
}