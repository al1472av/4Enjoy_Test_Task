using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using LifeGame.GameData;
using LifeGame.Services;
using LifeGame.Services.GameData;
using LifeGame.Services.PlayerData;
using LifeGame.Services.Timer;
using LifeGame.UI.Popups.Lives.Views;
using LifeGame.UI.Popups.Lives.Views.Args;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LifeGame.UI.Popups.Lives
{
    public class LivesPopup : PopupBase
    {
        private const string OUT_CLIP_NAME = "Out";
        private const string IN_CLIP_NAME = "In";
        
        [SerializeField] private FullLivesPopupView _fullLivesView;
        [SerializeField] private NoLivesPopupView _noLivesView;
        [SerializeField] private NotFullLivesPopupView _notFullLivesView;
        [SerializeField] private Animation _animation;
        private DataAccessProvider _data;

        private IPlayerDataService PlayerDataService => ServiceProvider.PlayerData;
        private IGameDataService GameDataService => ServiceProvider.GameData;
        private ITimeService TimeService => ServiceProvider.TimeService;

        public override UniTask Initialize()
        {
            base.Initialize();
            SubscribeOnEvents();
            _data = PlayerDataService.Data;
            return UniTask.CompletedTask;
        }

        public override async UniTask Show()
        {
            await base.Show();
            _animation.Play(IN_CLIP_NAME);
            RefreshView();
        }

        public override async UniTask Hide()
        {
            _animation.Play(OUT_CLIP_NAME);
            await UniTask.Delay((int)(1000 * _animation[OUT_CLIP_NAME].length));
            base.Hide();
        }

        private void RefreshView()
        {
            var config = GameDataService.Config;

            HideAll();

            if (_data.Lives == config.MinLives)
                ShowNoLivesView(config);
            else if (_data.Lives >= config.MaxLives)
                ShowFullLivesView(config);
            else
                ShowNotFullLivesView(config);
        }

        private void ShowNotFullLivesView(Config config)
        {
            var timer = PlayerDataService.Data.LivesTimer;
            timer.OnOverTime(OnTimerOver);
            NotFullLivesViewArgs args = new NotFullLivesViewArgs(_data.Lives, timer);
            ShowView(_notFullLivesView, args);
        }

        private void ShowFullLivesView(Config config)
        {
            FullLivesViewArgs args = new FullLivesViewArgs(config.MaxLives);
            ShowView(_fullLivesView, args);
        }

        private void ShowNoLivesView(Config config)
        {
            var timer = PlayerDataService.Data.LivesTimer;
            timer.OnOverTime(OnTimerOver);
            NoLivesViewArgs args = new NoLivesViewArgs(config.MinLives, timer);
            ShowView(_noLivesView, args);
        }

        private void ShowView(LivesPopupViewBase view, LivesViewArgsBase args)
        {
            view.SetViewData(args);
            view.Show();
        }

        private void HideAll()
        {
            _notFullLivesView.Hide();
            _fullLivesView.Hide();
            _noLivesView.Hide();
        }

        private void SubscribeOnEvents()
        {
            _fullLivesView.UseLifeButton.onClick.AddListener(OnUseLife);
            _notFullLivesView.UseLifeButton.onClick.AddListener(OnUseLife);
            _notFullLivesView.RefillLifeButton.onClick.AddListener(OnRefillLife);
            _noLivesView.RefillLifeButton.onClick.AddListener(OnRefillLife);
            PlayerDataService.Data.LivesChanged += OnLivesChanged;
        }

        private void OnUseLife()
        {
            _data.Lives -= 1;
        }

        private void OnRefillLife()
        {
            _data.Lives += 1;
        }

        private void OnLivesChanged(int _)
        {
            RefreshView();
        }

        private void OnTimerOver()
        {
            RefreshView();
        }
    }
}