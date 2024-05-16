using System;
using System.Threading.Tasks;
using Proyecto26;
using Workspace.Extensions;
using Workspace.Services.Persistence.Data;

namespace Workspace.Networking.Time
{
    public static class WorldTimeApi
    {
        private const string ApiUrl = "http://worldtimeapi.org/api/ip";
        
        public static async Task<DateTime> GetTimeByIp()
        {
            var worldTime = await RestClient.Get<WorldTimeData>(ApiUrl).ToTask();
            
            return worldTime.ToDateTime();
        }
    }
}