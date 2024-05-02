using System;
using Better.Services.Runtime;

namespace Workspace.Installers.Services
{
    [Serializable]
    public abstract class DisposablePocoService : PocoService, IDisposable
    {
        public abstract void Dispose();
    }
}