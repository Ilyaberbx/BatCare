using UnityEditor;
using Workspace.Scripts.Editor;

namespace Workspace.Editor.Progress
{
    public static class ProgressToolsProvider
    {
        [MenuItem(EditorPaths.ProgressPath + "Clear Progress")]
        private static void Clear()
        {
            ProgressUtility.Clear();
        }
        
        [MenuItem(EditorPaths.ProgressPath + "Open In Explorer")]
        private static void OpenProgressFolder()
        {
            ProgressUtility.OpenProgressFolder();
        }
    }
}