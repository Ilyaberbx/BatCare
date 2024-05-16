using System;
using System.Threading;
using System.Threading.Tasks;

namespace Workspace.Services.Location.Abstractions
{
    public interface ILocationSystem<in TDerivedLocation> where TDerivedLocation : BaseLocation
    {
        Task Enter<TLocation>(CancellationToken token) where TLocation : TDerivedLocation;
        Task Enter(Type type, CancellationToken token);
        void Enter<TLocation>() where TLocation : TDerivedLocation;

        void Enter(Type type);

        Task Exit(CancellationToken token);
    }
}