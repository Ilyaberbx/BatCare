using Newtonsoft.Json;

namespace Workspace.Extensions
{
    public static class JsonExtensions
    {
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}