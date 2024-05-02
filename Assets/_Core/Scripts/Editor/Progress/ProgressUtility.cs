using System.IO;
using Better.Saves.Runtime.Settings;
using UnityEngine;

namespace Workspace.Editor.Progress
{
    public static class ProgressUtility
    {
        public static void Clear()
        {
            var folderPath = SavesSettings.Instance.GetFolderPath();
            Directory.Delete(Path.Combine(Application.persistentDataPath, folderPath), true);
        }
    }
}