using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.SceneManagement.Runtime;
using Better.SceneManagement.Runtime.Interfaces;
using Better.SceneManagement.Runtime.Transitions;
using Better.Services.Runtime;
using UnityEngine;
using Workspace.Services.SceneManagement.Data;

namespace Workspace.Services.SceneManagement
{
    //TODO: Remake using event bus approach
    public class ScenesWrapperService : PocoService, ISceneSystem
    {
        [SerializeField] private LoadingCurtain _curtain;

        private InternalSceneSystem _sceneSystem;
        private SceneGroup _currentGroup;
        
        private readonly Dictionary<SceneReference, float> _progressByReference = new();

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _sceneSystem = new InternalSceneSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task LoadGroup(SceneGroup group)
        {
            _currentGroup = group;
            
            await _curtain.Show();

            var additiveTransition = _sceneSystem
                .CreateAdditiveTransition()
                .LoadScenes(group.SceneReferences);

            SubscribeProgressUpdates(additiveTransition);

            await additiveTransition.RunAsync();

            _progressByReference.Clear();
            
            _curtain.Hide().Forget();
        }

        private void SubscribeProgressUpdates(AdditiveTransitionInfo additiveTransition)
        {
            foreach (var sceneReference in _currentGroup.SceneReferences)
            {
                _progressByReference.Add(sceneReference, 0f);
                additiveTransition.OnProgress(sceneReference, (_, progress) => UpdateCurtain(sceneReference, progress));
            }
        }

        private void UpdateCurtain(SceneReference sceneReference, float progress)
        {
            _progressByReference[sceneReference] = progress;

            var averageProgress = _progressByReference.Values.Average();
            
            _curtain.ShowProgress(averageProgress, _progressByReference.Values.Count);
        }

        public SingleTransitionInfo CreateSingleTransition(SceneReference sceneReference) => _sceneSystem.CreateSingleTransition(sceneReference);

        public AdditiveTransitionInfo CreateAdditiveTransition() => _sceneSystem.CreateAdditiveTransition();
    }
}