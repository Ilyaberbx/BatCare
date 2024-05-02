using System.Threading.Tasks;
using RSG;

namespace Workspace.Extensions
{
    public static class RestClientExtensions
    {
        public static Task<T> ToTask<T>(this IPromise<T> promise)
        {
            var source = new TaskCompletionSource<T>();
            
            promise
                .Then(response => source.TrySetResult(response))
                .Catch(error => source.SetException(error));

            return source.Task;
        }
    }
}