using System.Diagnostics;
using System.IO;
using Better.Saves.Runtime.Settings;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Workspace.Editor.Progress
{
    public static class ProgressUtility
    {
        private static string ProgressPath => SavesSettings.Instance.GetFolderPath();

        public static void Clear() => Directory.Delete(ProgressPath, true);

        public static void OpenProgressFolder()
        {
            if (Directory.Exists(ProgressPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = ProgressPath,
                    FileName = @"c:\windows\explorer.exe"
                };
                
                Debug.Log(ProgressPath);

                Process.Start(startInfo);
            }
        }
    }
}