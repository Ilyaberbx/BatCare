using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Workspace.Services.Location.Abstractions;

namespace Workspace.Services.Location.Implementations.Room
{
    public class RoomsService : PocoService<RoomsSettings>, ILocationSystem<Room>
    {
        public Task Enter<TLocation>(CancellationToken token) where TLocation : Room
        {
            throw new NotImplementedException();
        }

        public Task Enter(Type type, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Enter<TLocation>() where TLocation : Room
        {
            throw new NotImplementedException();
        }

        public void Enter(Type type)
        {
            throw new NotImplementedException();
        }

        public Task Exit(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}