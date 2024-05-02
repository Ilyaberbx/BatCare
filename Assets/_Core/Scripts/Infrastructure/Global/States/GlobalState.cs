using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Workspace.Services.SceneManagement;
using Workspace.Services.SceneManagement.Data;
using Workspace.Utilities;

namespace Workspace.Infrastructure.Global.States
{
    public abstract class GlobalState : InfrastructureState
    {
        private readonly ServiceProperty<ScenesWrapperService> _sceneServiceProperty = new();
        private SceneGroup _sceneGroup;

        protected ScenesWrapperService SceneService => _sceneServiceProperty.CachedService;

        public void SetData(SceneGroup sceneGroup)
        {
            _sceneGroup = sceneGroup;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var loadingTask = SceneService.LoadGroup(_sceneGroup);
            return loadingTask;
        }

        public override Task ExitAsync(CancellationToken token)
        {
            UIFacadeUtility.Clear();
            
            var unloadingTask = SceneService
                .CreateAdditiveTransition()
                .UnloadAllScenes()
                .RunAsync();

            return unloadingTask;
        }
    }
}