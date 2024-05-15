using System.Threading;
using System.Threading.Tasks;
using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Components.UI;
using UnityEngine;
using UnityEngine.UI;
using Workspace.Core.Actions.Abstractions;
using Workspace.Extensions;

namespace Workspace.Services.SceneManagement
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingCurtainSystem : UIMonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;

        [SerializeReference, Select] private VisualAction[] _onShowActions;
        [SerializeReference, Select] private VisualAction[] _onHideActions;

        private CancellationTokenSource _tokenSource;

        public void Initialize()
        {
            _tokenSource = new CancellationTokenSource();
            
            _onHideActions.Initialize();
            _onShowActions.Initialize();
        }

        private void OnDestroy() => _tokenSource?.Cancel();

        public Task Show() => _onShowActions?.Execute(_tokenSource.Token);

        public Task Hide() => _onHideActions?.Execute(_tokenSource.Token);

        public void ShowProgress(float progress, float ratio) => _image.fillAmount = progress / ratio;
    }
}