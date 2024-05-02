using UnityEngine;
using UnityEngine.SceneManagement;

namespace Workspace.Utilities.Application
{
    public static class ApplicationBootstapper
    {
        private const string CoreScene = "Core";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ApplySettings()
        {
            UnityEngine.Application.runInBackground = true;
            UnityEngine.Application.targetFrameRate = 60;

            if (SceneManager.GetActiveScene().name != CoreScene)
                SceneManager.LoadSceneAsync(CoreScene, LoadSceneMode.Single);
        }
    }
}