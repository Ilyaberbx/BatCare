using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Workspace.Services.Location.Abstractions;

namespace Workspace.Services.Location.Implementations.Room
{
    public class RoomsService : PocoService<RoomsSettings>, ILocationSystem<Room>
    {
        private ILocationSystem<Room> _internalLocationSystem;

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            _internalLocationSystem = new InternalLocationSystem<Room>(Settings.Data.ToDictionary(data => data.Type, data => data.Reference));
        }

        #region ILocationSystem

        public Task Enter<TLocation>(CancellationToken token) where TLocation : Room => _internalLocationSystem.Enter<TLocation>(token);

        public Task Enter(Type type, CancellationToken token) => _internalLocationSystem.Enter(type, token);

        public void Enter<TLocation>() where TLocation : Room => _internalLocationSystem.Enter<TLocation>();

        public void Enter(Type type) => _internalLocationSystem.Enter(type);

        public Task Exit(CancellationToken token) => _internalLocationSystem.Exit(token);

        #endregion
    }
}