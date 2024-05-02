using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;

namespace Workspace.Extensions
{
    public static class TweenExtensions
    {
        public static Task ToTask(this Tween tween, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            tween.OnComplete(() => { taskCompletionSource.TrySetResult(true); });
            tween.OnKill(() => { taskCompletionSource.TrySetCanceled(); });

            if (cancellationToken != CancellationToken.None)
            {
                tween.OnUpdate(() =>
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        tween.Kill();
                    }
                });
            }
           
            return taskCompletionSource.Task;
        }
    }
}