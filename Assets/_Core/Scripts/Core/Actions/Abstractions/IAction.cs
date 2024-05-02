using System.Threading;
using System.Threading.Tasks;

namespace Workspace.Core.Actions.Abstractions
{
    public interface IAction
    {
        void Initialize();
        Task Execute(CancellationToken token);
    }
}