using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Locators.Runtime;
using Better.SceneManagement.Runtime;
using Workspace.Services.SceneManagement;
using Workspace.Services.SceneManagement.Data;
using Workspace.Utilities;

namespace Workspace.Infrastructure.Global.States
{
    public abstract class GlobalState : InfrastructureState
    {
        private readonly ServiceProperty<SceneService> _sceneServiceProperty = new();
        private SceneService SceneService => _sceneServiceProperty.CachedService;

        private SceneGroup _sceneGroup;
        
        private readonly Dictionary<SceneReference, float> _progressByReference = new();

        public void SetData(SceneGroup sceneGroup)
        {
            _sceneGroup = sceneGroup;
        }

        public override Task EnterAsync(CancellationToken token)
        {
            var transitionInfo = SceneService
                .CreateAdditiveTransition()
                .LoadScenes(_sceneGroup.References);
            
            foreach (var sceneReference in _sceneGroup.References)
            {
                _progressByReference.Add(sceneReference, 0f);
                transitionInfo.OnProgress(sceneReference, (sender, progress) => OnProgressChanged(sceneReference, progress));
            }

            return Task.CompletedTask;
        }

        private void OnProgressChanged(SceneReference reference, float progress)
        {
            _progressByReference[reference] = progress;

            var averageProgress = _progressByReference.Values.Average();
            
//            CurtainService.ShowProgress(averageProgress, _progressByReference.Values.Count);
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