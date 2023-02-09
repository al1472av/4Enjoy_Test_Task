using Cysharp.Threading.Tasks;
using LifeGame.GameData;
using LifeGame.Services.Addressable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LifeGame.Services.GameData
{
    public class GameDataService : ServiceBase, IGameDataService
    {

        [SerializeField] private AssetReferenceT<Config> _configReference;
        public Config Config { get; private set; }

        private IAddressablesService AddressablesService => ServiceProvider.Addressables;
        
        public override async UniTask StartAsync()
        {
            Config = await AddressablesService.LoadAsync<Config>(_configReference);
        }

    }
}