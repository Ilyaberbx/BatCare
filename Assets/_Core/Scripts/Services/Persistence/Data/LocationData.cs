using System;

namespace Workspace.Services.Persistence.Data
{
    [Serializable]
    public class LocationData
    {
        public int Index;

        public LocationData(int index)
        {
            Index = index;
        }
    }
}