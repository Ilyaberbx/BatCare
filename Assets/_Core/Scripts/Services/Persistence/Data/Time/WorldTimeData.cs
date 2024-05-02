using System;
using Newtonsoft.Json;

namespace Workspace.Services.Persistence.Data.Time
{
    [Serializable]
    public class WorldTimeData
    {
        [JsonProperty("datetime")]
        public string DateTime;
    }
}