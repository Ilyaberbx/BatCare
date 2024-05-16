using System;
using Newtonsoft.Json;

namespace Workspace.Services.Persistence.Data
{
    [Serializable]
    public class WorldTimeData
    {
        [JsonProperty("datetime")]
        public string DateTime;
    }
}