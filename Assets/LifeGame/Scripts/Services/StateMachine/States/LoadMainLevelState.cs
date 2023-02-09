using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using LifeGame.Services.Factory;
using LifeGame.Services.GameData;
using LifeGame.Services.PlayerData;
using LifeGame.Services.SceneLoader;
using LifeGame.Services.UI;

namespace LifeGame.Services.StateMachine.States
{
    public class LoadMainLevelState : IState
    {
        private ISceneLoaderService SceneLoaderService => ServiceProvider.SceneLoader;
        private IGameStateMachineService GameStateMachineService => ServiceProvider.StateMachine;
        private IPlayerDataService PlayerDataService => ServiceProvider.PlayerData;
        private IUIFactoryService UIFactoryService => ServiceProvider.UIFactory;
        private IUIService UIService => ServiceProvider.UIService;

        public async void Enter()
        {
            await SceneLoaderService.Load("Main");
            await UIFactoryService.CreateAndInitializeUIRoot();
            await CreateAndShowMenu();
            OpenDailyIfNotClaimed();
            GameStateMachineService.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }

        private async Task CreateAndShowMenu()
        {
            var mainMenu = await UIFactoryService.CreateMainMenu();
            UIService.AddWindow(mainMenu);
            UIService.OpenWindow(mainMenu);
        }

        private async void OpenDailyIfNotClaimed()
        {
            if (!PlayerDataService.Data.DailyClaimed.Contains(DateTime.Today))
            {
                var dailyPopup = await UIFactoryService.CreateDailyRewardPopup(null);
                UIService.AddPopup(dailyPopup);
                UIService.ShowPopup(dailyPopup);
            }
        }
    }
}