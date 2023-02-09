using Cysharp.Threading.Tasks;
using LifeGame.Services.Addressable;
using LifeGame.UI.Popups;
using LifeGame.UI.Popups.DailyReward;
using LifeGame.UI.Popups.Lives;
using LifeGame.UI.Windows;
using LifeGame.UI.Windows.MainMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LifeGame.Services.Factory
{
    public class UIFactoryService : ServiceBase, IUIFactoryService
    {
        //TODO: Separate Windows and Popups to different fabrics
        [SerializeField] private AssetReferenceT<GameObject> _uiRootReference;
        [SerializeField] private AssetReferenceComponent<MainMenuWindow> _mainMenuReference;
        [SerializeField] private AssetReferenceComponent<LivesPopup> _livesPopupReference;
        [SerializeField] private AssetReferenceComponent<DailyRewardPopup> _dailyRewardPopupReference;
        private Transform _uiRoot;
        private IAddressablesService AddressablesService => ServiceProvider.Addressables;

        public async UniTask CreateAndInitializeUIRoot()
        {
            _uiRoot = (await AddressablesService.Instantiate(_uiRootReference)).transform;
        }

        public async UniTask<MainMenuWindow> CreateMainMenu()
        {
            return await LoadAndInstantiate(_mainMenuReference, _uiRoot);
        }

        public async UniTask<LivesPopup> CreateLivesPopup(Transform parent = null)
        {
            var tempParent = parent == null ? _uiRoot : parent;
            return await LoadAndInstantiate(_livesPopupReference, tempParent);
        }

        public async UniTask<DailyRewardPopup> CreateDailyRewardPopup(Transform parent = null)
        {
            var tempParent = parent == null ? _uiRoot : parent;
            return await LoadAndInstantiate(_dailyRewardPopupReference, tempParent);
        }

        private async UniTask<T> LoadAndInstantiate<T>(AssetReferenceT<T> key, Transform at) where T : Object
        {
            return (await AddressablesService.Instantiate(key, at)).GetComponent<T>();
        }
    }
}