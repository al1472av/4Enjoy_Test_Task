using LifeGame.Services.Addressable;
using LifeGame.Services.Factory;
using LifeGame.Services.GameData;
using LifeGame.Services.Loading;
using LifeGame.Services.PlayerData;
using LifeGame.Services.SceneLoader;
using LifeGame.Services.StateMachine;
using LifeGame.Services.Timer;
using LifeGame.Services.UI;
using LifeGame.Services.UnityEvent;

namespace LifeGame.Services
{
    //TODO Implement logic of changing service implementation at runtime,
    //but not necessary in this test task, so i just return main implementation  
    
    public static class ServiceProvider
    {
        public static IPlayerDataService PlayerData => ServiceLocator.GetService<PlayerDataService>();
        public static IGameDataService GameData => ServiceLocator.GetService<GameDataService>();
        public static ISceneLoaderService SceneLoader => ServiceLocator.GetService<SceneLoaderService>();
        public static ILoaderService Loading => ServiceLocator.GetService<LoaderService>();
        public static IAddressablesService Addressables => ServiceLocator.GetService<AddressablesService>();
        public static IGameStateMachineService StateMachine => ServiceLocator.GetService<GameStateMachineService>();
        public static IUIFactoryService UIFactory => ServiceLocator.GetService<UIFactoryService>();
        public static IUIService UIService => ServiceLocator.GetService<UIService>();
        public static ITimeService TimeService => ServiceLocator.GetService<TimeService>();
        public static IUnityEventService UnityEventService => ServiceLocator.GetService<UnityEventService>();
    }
}