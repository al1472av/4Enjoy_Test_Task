using Cysharp.Threading.Tasks;
using LifeGame.Services;
using LifeGame.Services.GameData;
using LifeGame.Services.PlayerData;
using LifeGame.UI.Popups.Lives;
using UnityEngine;

namespace LifeGame.UI.Windows.MainMenu
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private LivesBarView _livesBarView;
        private LivesPopup _livesPopup;
        private IPlayerDataService PlayerDataService => ServiceProvider.PlayerData;

        public override async UniTask Initialize()
        {
            _livesBarView.Initialize(PlayerDataService.Data.LivesTimer);
            SubscribeOnEvents();
            RefreshView(PlayerDataService.Data.Lives);

            await CreateAndInitializeLivesPopup();
        }

        private async UniTask CreateAndInitializeLivesPopup()
        {
            _livesPopup = await UIFactoryService.CreateLivesPopup(transform);
            _livesPopup.HardClose();
            _livesPopup.Initialize();
        }

        private void RefreshView(int value)
        {
            _livesBarView.SetViewData(value);
        }

        private void SubscribeOnEvents()
        {
            _livesBarView.Button.onClick.AddListener(OnLivesBarButtonClicked);
            PlayerDataService.Data.LivesChanged += OnLivesChanged;
        }

        private void OnLivesChanged(int value)
        {
            RefreshView(value);
        }

        private void OnLivesBarButtonClicked()
        {
            _livesPopup.Show();
        }
    }
}