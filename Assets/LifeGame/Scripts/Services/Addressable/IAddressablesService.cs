using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LifeGame.Services.Addressable
{
    public interface IAddressablesService : IService
    {
        UniTask<T> LoadAsync<T>(AssetReference assetReference) where T : class;

        void Release(AssetReference assetReference);
        UniTask<GameObject> Instantiate(AssetReference address, Transform parent = null);
    }
}