using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LifeGame.Services.Addressable
{
    public class AddressablesService : ServiceBase, IAddressablesService
    {
        private Dictionary<string, AsyncOperationHandle> _cachedObjects;

        public override async UniTask InitializeAsync()
        {
            _cachedObjects = new Dictionary<string, AsyncOperationHandle>();
            await Addressables.InitializeAsync();
        }

        public async UniTask<T> LoadAsync<T>(AssetReference assetReference) where T : class
        {
            string key = assetReference.AssetGUID;

            if (_cachedObjects.ContainsKey(key))
                return _cachedObjects[key].Result as T;

            var loadingOperation = Addressables.LoadAssetAsync<T>(assetReference);

            await loadingOperation;

            _cachedObjects.Add(key, loadingOperation);

            return _cachedObjects[key].Result as T;
        }

        public async UniTask<GameObject> Instantiate(AssetReference address, Transform parent = null)
        {
            return await Addressables.InstantiateAsync(address, parent).Task;
        }

        public void Release(AssetReference assetReference)
        {
            string key = assetReference.AssetGUID;

            Addressables.Release(_cachedObjects[key]);

            _cachedObjects.Remove(key);
        }
    }
}