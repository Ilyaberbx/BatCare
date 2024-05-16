using System;
using Better.Commons.Runtime.DataStructures.SerializedTypes;

namespace Workspace.Services.Persistence.Data
{
    [Serializable]
    public class WallpapersData
    {
        public SerializedDictionary<int, int> WallpapersByRoom;

        public WallpapersData()
        {
            WallpapersByRoom = new SerializedDictionary<int, int>();
        }
    }
}