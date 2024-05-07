using System.Threading.Tasks;
using Better.Locators.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Workspace.Services.Assets;

namespace Workspace.Utilities
{
    public static class AssetsUtility
    {
        private static ServiceProperty<AssetsService> _assetsProperty;
        
        private static AssetsService Assets => _assetsProperty.CachedService;

        public static async Task<T> Create<T>(string address) where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(address);
            GameObject createdObject = Object.Instantiate(prefab);
            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(string address, Vector3 at, Transform container) where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(address);
            GameObject createdObject = Object.Instantiate(prefab, at, Quaternion.identity, container);
            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(string address, Vector3 at, Quaternion rotation, Transform container)
            where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(address);
            GameObject createdObject = Object.Instantiate(prefab, at, rotation, container);
            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(string address, Vector3 at, bool selfRotation, Transform container)
            where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(address);
            GameObject createdObject = Object.Instantiate(prefab, at,
                selfRotation ? prefab.transform.localRotation : Quaternion.identity, container);

            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(AssetReference reference, Vector3 at, bool selfRotation,
            Transform container)
            where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(reference);
            GameObject createdObject = Object.Instantiate(prefab, at,
                selfRotation ? prefab.transform.localRotation : Quaternion.identity, container);

            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(string address, Transform container) where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(address);
            GameObject createdObject = Object.Instantiate(prefab, container);
            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(AssetReference reference) where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(reference);
            GameObject createdObject = Object.Instantiate(prefab);
            return createdObject.GetComponent<T>();
        }

        public static Task<T> Create<T>(AssetReference reference, Vector3 at, Quaternion rotation, Transform container)
            where T : Object => Create<T>(reference.AssetGUID, at, rotation, container);

        public static async Task<T> Create<T>(AssetReference reference, Vector3 at, Transform container)
            where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(reference);
            GameObject createdObject = Object.Instantiate(prefab, at, Quaternion.identity, container);
            return createdObject.GetComponent<T>();
        }

        public static async Task<T> Create<T>(AssetReference reference, Transform container) where T : Object
        {
            GameObject prefab = await Assets.Load<GameObject>(reference);
            GameObject createdObject = Object.Instantiate(prefab, container);
            return createdObject.GetComponent<T>();
        }
        
    }
}