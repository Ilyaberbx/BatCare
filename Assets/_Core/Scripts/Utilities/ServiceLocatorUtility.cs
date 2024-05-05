using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Utility;
using Better.Locators.Runtime;
using Better.Services.Runtime.Interfaces;
using Workspace.Services.Time;

namespace Workspace.Utilities
{
    public static class ServiceLocatorUtility
    {
        public static async Task<T> WaitForService<T>(CancellationToken token) where T : IService
        {
            await TaskUtility.WaitUntil(ServiceLocator.HasRegistered<T>, token);
            return ServiceLocator.Get<T>();
        }
    }
}