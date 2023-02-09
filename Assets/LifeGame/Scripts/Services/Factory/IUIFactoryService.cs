using Cysharp.Threading.Tasks;
using LifeGame.UI.Popups;
using LifeGame.UI.Popups.DailyReward;
using LifeGame.UI.Popups.Lives;
using LifeGame.UI.Windows;
using LifeGame.UI.Windows.MainMenu;
using UnityEngine;

namespace LifeGame.Services.Factory
{
    public interface IUIFactoryService : IService
    {
        UniTask<MainMenuWindow> CreateMainMenu();
        UniTask<LivesPopup> CreateLivesPopup(Transform parent);
        UniTask<DailyRewardPopup> CreateDailyRewardPopup(Transform parent);
        UniTask CreateAndInitializeUIRoot();
    }
}