using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.Infrastructure.Services.AssetManagement
{
    public interface IAssetProvider : IService
    {
        void CleanUp();
        void Initialize();
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);

        Task<GameObject> InstantiateAsync(string path);
        Task<GameObject> InstantiateAsync(string path, Vector3 position);
        Task<GameObject> InstantiateAsync(string address, Transform parent);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
    }
}