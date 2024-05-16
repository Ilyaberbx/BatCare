using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Workspace.Services.Location.Abstractions
{
    public abstract class BaseLocation : MonoBehaviour
    {
        public abstract Task Enter(CancellationToken token);
        public abstract Task Exit(CancellationToken token);
    }
}