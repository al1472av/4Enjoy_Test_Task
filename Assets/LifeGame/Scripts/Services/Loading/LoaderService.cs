using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LifeGame.Services.Addressable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LifeGame.Services.Loading
{
    public class LoaderService : ServiceBase, ILoaderService
    {
        [SerializeField] private AssetReferenceComponent<LoadingPanel> _loadingPanelPrefab;
        private LoadingPanel _loadingPanel;

        public override async UniTask InitializeAsync()
        {
            _loadingPanel = (await Addressables.InstantiateAsync(_loadingPanelPrefab)).GetComponent<LoadingPanel>();
        }

        public async UniTask Load(Queue<ILoadingOperation> loadingOperations)
        {
            await _loadingPanel.Load(loadingOperations);
        }

        public async UniTask Load(ILoadingOperation loadingOperation)
        {
            var loadingOperations = new Queue<ILoadingOperation>();
            loadingOperations.Enqueue(loadingOperation);
            await _loadingPanel.Load(loadingOperations);
        }

    }
}