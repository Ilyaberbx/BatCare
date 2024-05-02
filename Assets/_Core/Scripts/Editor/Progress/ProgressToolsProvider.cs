using UnityEditor;

namespace Workspace.Editor.Progress
{
    public static class ProgressToolsProvider
    {
        [MenuItem(EditorPaths.ProgressPath + "Clear Progress")]
        private static void Clear()
        {
            ProgressUtility.Clear();
        }
    }
}