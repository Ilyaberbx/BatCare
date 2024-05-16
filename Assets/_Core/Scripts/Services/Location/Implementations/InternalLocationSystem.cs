using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Commons.Runtime.Utility;
using UnityEngine.AddressableAssets;
using Workspace.Services.Location.Abstractions;
using Workspace.Utilities;

namespace Workspace.Services.Location.Implementations
{
    public class InternalLocationSystem<TDerivedLocation> : ILocationSystem<TDerivedLocation> where TDerivedLocation : BaseLocation
    {
        private readonly IReadOnlyDictionary<Type, AssetReference> _locationReferenceByType;

        private BaseLocation _currentLocation;

        public BaseLocation CurrentLocation => _currentLocation;

        public InternalLocationSystem(IReadOnlyDictionary<Type, AssetReference> locationReferenceReferenceByType)
        {
            _locationReferenceByType = locationReferenceReferenceByType;
        }
        
        public async Task Enter<TLocation>(CancellationToken token) where TLocation : TDerivedLocation
        {
            var type = typeof(TLocation);

            await Enter(type, token);
        }

        public async Task Enter(Type type, CancellationToken token)
        {
            if (_locationReferenceByType.TryGetValue(type, out var reference))
            {
                var location = await AssetsUtility.Create<BaseLocation>(reference);

                await Exit(token);

                _currentLocation = location;

                await _currentLocation.Enter(token);
            }
            else
            {
                DebugUtility.LogException<InvalidOperationException>();
            }
        }

        public void Enter<TLocation>() where TLocation : TDerivedLocation
        {
            Enter<TLocation>(CancellationToken.None).Forget();
        }

        public void Enter(Type type)
        {
            Enter(type, CancellationToken.None).Forget();
        }

        public Task Exit(CancellationToken token)
        {
            return _currentLocation != null ? _currentLocation.Exit(token) : Task.CompletedTask;
        }
    }
}