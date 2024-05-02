using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Workspace.Services.Assets
{
    public class AssetsService : PocoService
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetRequests = new();
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            return Addressables.InitializeAsync().Task;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task<TAsset> Load<TAsset>(string key) where TAsset : class
        {
            if (!_assetRequests.TryGetValue(key, value: out var handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _assetRequests.Add(key, handle);
            }

            await handle.Task;

            return handle.Result as TAsset;
        }

        public async Task<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class
        {
            return await Load<TAsset>(assetReference.AssetGUID);
        }

        public IAsyncEnumerable<string> GetAssetsListByLabel<TAsset>(string label)
        {
            return GetAssetsListByLabel(label, typeof(TAsset));
        }

        public async IAsyncEnumerable<string> GetAssetsListByLabel(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);

            var locations = await operationHandle.Task;

            foreach (IResourceLocation location in locations)
                yield return location.PrimaryKey;

            Addressables.Release(operationHandle);
        }

        public async Task<TAsset[]> LoadAll<TAsset>(IEnumerable<string> keys) where TAsset : class
        {
            var tasks = new List<Task<TAsset>>();

            foreach (var key in keys)
                tasks.Add(Load<TAsset>(key));

            return await Task.WhenAll(tasks);
        }

        public async Task<TAsset[]> LoadAll<TAsset>(IAsyncEnumerable<string> keys) where TAsset : class
        {
            var tasks = new List<Task<TAsset>>();

            await foreach (var key in keys)
                tasks.Add(Load<TAsset>(key));

            return await Task.WhenAll(tasks);
        }


        public async Task WarmUpAssetsByLabel(string label)
        {
            var assetsList = GetAssetsListByLabel(label);
            await LoadAll<object>(assetsList);
        }

        public async Task ReleaseAssetsByLabel(string label)
        {
            var assetsList = GetAssetsListByLabel(label);

            await foreach (var assetKey in assetsList)
                if (_assetRequests.TryGetValue(assetKey, out var handler))
                {
                    Addressables.Release(handler);
                    _assetRequests.Remove(assetKey);
                }
        }

        public void CleanUp()
        {
            foreach (var assetRequest in _assetRequests)
                Addressables.Release(assetRequest.Value);

            _assetRequests.Clear();
        }
    }
}