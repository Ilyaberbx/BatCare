using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Workspace
{
    public class ApplicationBootstapper : MonoBehaviour
    {
        private const string CoreScene = "Core";
        
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
            
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }
    }
}