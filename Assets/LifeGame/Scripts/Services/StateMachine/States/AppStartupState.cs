using LifeGame.Services.SceneLoader;

namespace LifeGame.Services.StateMachine.States
{
    public class AppStartupState : IState
    {
        private IGameStateMachineService StateMachineService => ServiceProvider.StateMachine;
        private ISceneLoaderService SceneLoaderService => ServiceProvider.SceneLoader;

        public void Enter()
        {
            //TODO: Need to be done some bootstrapping operation, not necessary yet, so we just go on
            StateMachineService.Enter<LoadMainLevelState>();
        }

        public void Exit()
        {
        }
    }
}