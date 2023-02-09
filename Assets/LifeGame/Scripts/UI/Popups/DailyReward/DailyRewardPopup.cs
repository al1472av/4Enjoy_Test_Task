using System;
using Cysharp.Threading.Tasks;
using LifeGame.Services;
using LifeGame.Services.GameData;
using LifeGame.Services.PlayerData;
using UnityEngine;

namespace LifeGame.UI.Popups.DailyReward
{
    public class DailyRewardPopup : PopupBase
    {
        private const string OUT_CLIP_NAME = "Out";
        private const string IN_CLIP_NAME = "In";

        [SerializeField] private DailyRewardPopupView _dailyRewardPopupView;
        [SerializeField] private Animation _animation;
        private IPlayerDataService PlayerDataService => ServiceProvider.PlayerData;
        private IGameDataService GameDataService => ServiceProvider.GameData;

        public override UniTask Initialize()
        {
            _dailyRewardPopupView.Button.onClick.AddListener(OnGetCoinsClicked);
            return base.Initialize();
        }

        public override UniTask Show()
        {
            _animation.Play(IN_CLIP_NAME);
            _dailyRewardPopupView.SetViewData(GameDataService.Config.GetDailyRewardForToday());
            return base.Show();
        }

        public override async UniTask Hide()
        {
            _animation.Play(OUT_CLIP_NAME);
            await UniTask.Delay((int)(1000 * _animation[OUT_CLIP_NAME].length));
            base.Hide();
        }

        private void OnGetCoinsClicked()
        {
            PlayerDataService.Data.Coins += GameDataService.Config.GetDailyRewardForToday();
            PlayerDataService.Data.DailyClaimed.Add(DateTime.Today);
            Hide().Forget();
        }
    }
}