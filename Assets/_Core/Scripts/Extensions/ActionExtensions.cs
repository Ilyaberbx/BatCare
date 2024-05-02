using System.Threading;
using System.Threading.Tasks;
using Workspace.Core.Actions.Abstractions;

namespace Workspace.Extensions
{
    public static class ActionExtensions
    {
        public static async Task Execute(this IAction[] actions, CancellationToken token)
        {
            foreach (var action in actions)
            {
                await action.Execute(token);
            }
        }
    }
}