using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets.Scripts.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public Task<GameObject> InstantiateAsync(string address, Vector3 at)
        {
            return Addressables.InstantiateAsync(address, at, Quaternion.identity).Task;
        }

        public Task<GameObject> InstantiateAsync(string address, Transform parent)
        {
            return Addressables.InstantiateAsync(address, parent).Task;
        }

        public Task<GameObject> InstantiateAsync(string address)
        {
            return Addressables.InstantiateAsync(address).Task;
        }



        public async Task<T> Load<T>(AssetReference assetReference) where T: class
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }

            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(assetReference), assetReference.AssetGUID);
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
            {
                return completedHandle.Result as T;
            }
            
            return await RunWithCacheOnComplete(Addressables.LoadAssetAsync<T>(address), address);
        }

        public void CleanUp()
        {
            foreach(List<AsyncOperationHandle> resourceHandles in _handles.Values)
            {
                foreach (AsyncOperationHandle handle in resourceHandles)
                {
                    Addressables.Release(handle);
                }
            }
            _completedCache.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += h =>
            {
                _completedCache[cacheKey] = h;
            };

            AddHandle(cacheKey, handle);

            return await handle.Task;
        }

        private void AddHandle<T>(string key, AsyncOperationHandle<T> handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourcedHandle))
            {
                resourcedHandle = new List<AsyncOperationHandle>();
                _handles[key] = resourcedHandle;
            }
            resourcedHandle.Add(handle);
        }
    }
}