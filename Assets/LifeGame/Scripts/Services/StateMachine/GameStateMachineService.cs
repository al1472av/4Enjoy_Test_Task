using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LifeGame.Services.StateMachine.States;

namespace LifeGame.Services.StateMachine
{
    public class GameStateMachineService : ServiceBase, IGameStateMachineService
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public override UniTask InitializeAsync()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(AppStartupState)] = new AppStartupState(),
                [typeof(LoadMainLevelState)] = new LoadMainLevelState(),
                [typeof(GameLoopState)] = new GameLoopState(),
            };
            return base.InitializeAsync();
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
}