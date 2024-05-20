using System.Threading;
using Better.Locators.Runtime;
using Better.SceneManagement.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Workspace.Utilities;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Workspace
{
    public class ApplicationBootstapper : MonoBehaviour
    {
        private const string CoreScene = "Core";

        [SerializeField] private SceneReference _gamePlayReference;

        private CancellationTokenSource _tokenSource;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ApplySettings()
        {
            Application.runInBackground = true;
            Application.targetFrameRate = 60;

            if (SceneManager.GetActiveScene().name != CoreScene)
                SceneManager.LoadSceneAsync(CoreScene, LoadSceneMode.Single);
        }

        private void Awake()
        {
            _tokenSource = new CancellationTokenSource();
        }

        private async void Start()
        {
            var sceneService = await ServiceLocator.GetAsync<SceneService>(_tokenSource.Token);

            await sceneService
                .CreateAdditiveTransition()
                .LoadScene(_gamePlayReference)
                .RunAsync();
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }
    }
}